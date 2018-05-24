using System;
using System.Threading.Tasks;
using EDF.TradeCapture.Messaging.MassTransit;
using EDF.TradeCapture.Persistence.DbContext;
using EDF.TradeCapture.Persistence.Model;
using EDF.TradeCapture.StateSaga.StateMachine;
using MassTransit;
using MassTransit.EntityFrameworkIntegration;
using MassTransit.EntityFrameworkIntegration.Saga;
using MassTransit.Saga;
using Topshelf;

namespace EDF.TradeCapture.StateSaga
{
    internal class SagaStateService : ServiceControl
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
            var quoteStateMachine = new TradeStateMachine();

            SagaDbContextFactory quoteDbContextFactory =
                () => new PersistedTradeDbContext();

            var quoteRepo = new Lazy<ISagaRepository<PersistedTrade>>(
                () => new EntityFrameworkSagaRepository<PersistedTrade>(quoteDbContextFactory));


            return BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(
                    host,
                    "trade_saga",
                    e =>
                    {
                        e.UseInMemoryOutbox();
                        e.StateMachineSaga(quoteStateMachine, quoteRepo.Value);
                    });
            });
        }

    }
}