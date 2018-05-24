using Autofac;
using EDF.TradeCapture.Common;
using Serilog;
using Topshelf;
using Topshelf.Autofac;

namespace EDF.TradeCapture.GeSwapPump
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

                cfg.Service<GeSwapPumpService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted((service, control) => service.Start(control));
                    s.WhenStopped((service, control) => service.Stop(control));
                });

                cfg.RunAsLocalService();
                cfg.SetDisplayName("GE Swap Pump");
                cfg.SetDescription("This service sends trades to the GE Swap Pump.");

                cfg.OnException(ex =>
                {
                    Log.Fatal(ex, "Encountered unhandled exception (bubbled to Topshelf)");
                });
            });
        }


        private static IContainer Initialise()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new GeSwapPumpServiceModule());

            return builder.Build();
        }
    }
}
