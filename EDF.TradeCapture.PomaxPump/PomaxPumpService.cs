using System.Threading.Tasks;
using EDF.TradeCapture.Messaging.MassTransit;
using MassTransit;
using Topshelf;

namespace EDF.TradeCapture.PomaxPump
{
    public class PomaxPumpService : ServiceControl
    {
        private IBusControl _bus;

        public bool Start(HostControl hostControl)
        {
            Task.Factory.StartNew(
                () =>
                {
                    _bus = ConfigureBus();
                    _bus.Start();
                });

            return true;
        }

        
        public bool Stop(HostControl hostControl)
        {
            return true;
        }


        private IBusControl ConfigureBus()
        {
            return BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.UseSerilog();

                cfg.ReceiveEndpoint(
                    host,
                    "pomax_data",
                    endpointConfigurator =>
                    {
                        endpointConfigurator.Consumer(() => new PomaxPumpConsumer());
                    }); ;
            });
        }
    }
}