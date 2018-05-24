using Autofac;
using EDF.TradeCapture.Common;
using Serilog;
using Topshelf;
using Topshelf.Autofac;

namespace EDF.TradeCapture.StateSaga
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

                cfg.Service<SagaStateService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted((service, control) => service.Start(control));
                    s.WhenStopped((service, control) => service.Stop(control));
                });

                cfg.RunAsLocalService();
                cfg.SetDisplayName("State Saga Service");
                cfg.SetDescription("This service facilitates the handling of state in the Trade Capture.");

                cfg.OnException(ex =>
                {
                    Log.Fatal(ex, "Encountered unhandled exception (bubbled to Topshelf)");
                });
            });
        }


        private static IContainer Initialise()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new SagaModule());

            return builder.Build();
        }
    }
}
