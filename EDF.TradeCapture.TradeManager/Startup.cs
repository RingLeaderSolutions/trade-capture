using System;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Threading;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using EDF.TradeCapture.Messaging.MassTransit;
using EDF.TradeCapture.ValidationService.Services;
using MassTransit;
using MassTransit.Util;
using Microsoft.Owin.BuilderProperties;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;

namespace EDF.TradeCapture.ValidationService
{
    public class Startup
    {
        private static IContainer Container { get; set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            app.UseCors(CorsOptions.AllowAll);

            ConfigureJsonSerialisation(config);

            app.UseWebApi(config);

            // Configure and start Autofac
            Container = ConfigureContainer(config);
            app.UseAutofacMiddleware(Container);

            app.UseAutofacWebApi(config);

            // Start Mass Transit Service bus, and register stopping of bus on app dispose
            var bus = Container.Resolve<IBusControl>();
            var busHandle = TaskUtil.Await(() => bus.StartAsync());

            var properties = new AppProperties(app.Properties);

            if (properties.OnAppDisposing != CancellationToken.None)
            {
                properties.OnAppDisposing.Register(() => busHandle.Stop(TimeSpan.FromSeconds(30)));
            }

            config.EnsureInitialized();
        }

        private static void ConfigureJsonSerialisation(HttpConfiguration config)
        {
            var jsonFormatter = new JsonMediaTypeFormatter();
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            serializerSettings.Converters.Add(new StringEnumConverter());

            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
        }


        private IContainer ConfigureContainer(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.Register(c => BusConfigurator.ConfigureBus((cfg, host) =>
                {
                    cfg.UseSerilog();

                    cfg.ReceiveEndpoint(
                        host,
                        "validation_data",
                        endpointConfigurator =>
                        {
                            endpointConfigurator.Consumer(() => new ValidationServiceConsumer());
                        });
                }))
                .As<IBus>()
                .As<IBusControl>()
                .As<ISendEndpointProvider>()
                .SingleInstance();
            

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }
    }
}