using Autofac;
using EDF.TradeCapture.Common;
using EDF.TradeCapture.ValidationService.Services;
using Serilog;
using Topshelf;
using Topshelf.Autofac;

namespace EDF.TradeCapture.ValidationService
{
    class Program
    {
        static void Main()
        {
            var loggingConfig = LoggingConfiguration.ConfigureLogger();
            HostFactory.Run(cfg =>
            {
                var container = Initialise();

                cfg.UseSerilog(loggingConfig);
                cfg.UseAutofacContainer(container);

                cfg.Service<TradeCaptureService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted((service, control) => service.Start(control));
                    s.WhenStopped((service, control) => service.Stop(control));
                });
                
                cfg.RunAsLocalService();
                cfg.SetDisplayName("Validation Service");
                cfg.SetDescription("This service handles trades from the trade capture clients.");

                cfg.OnException(ex =>
                {
                    Log.Fatal(ex, "Encountered unhandled exception (bubbled to Topshelf)");
                });
            });
        }


        private static IContainer Initialise()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new TradeManagerModule());

            return builder.Build();
        }
    }
}
