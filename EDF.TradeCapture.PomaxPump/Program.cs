using Autofac;
using EDF.TradeCapture.Common;
using Serilog;
using Topshelf;
using Topshelf.Autofac;

namespace EDF.TradeCapture.PomaxPump
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggingConfig = LoggingConfiguration.ConfigureLogger();
            HostFactory.Run(cfg =>
            {
                var container = Initialise();

                cfg.UseSerilog(loggingConfig);
                cfg.UseAutofacContainer(container);

                cfg.Service<PomaxPumpService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted((service, control) => service.Start(control));
                    s.WhenStopped((service, control) => service.Stop(control));
                });

                cfg.RunAsLocalService();
                cfg.SetDisplayName("Pomax Pump");
                cfg.SetDescription("This service sends trades to the Pomax Pump.");

                cfg.OnException(ex =>
                {
                    Log.Fatal(ex, "Encountered unhandled exception (bubbled to Topshelf)");
                });
            });
        }


        private static IContainer Initialise()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new PomaxPumpServiceModule());

            return builder.Build();
        }
    }
}
