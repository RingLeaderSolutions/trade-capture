using EDF.TradeCapture.Persistence.Model;
using MassTransit.EntityFrameworkIntegration;

namespace EDF.TradeCapture.Persistence.DbContext
{
    public class PersistedTradeMap : SagaClassMapping<PersistedTrade>
    {
        public PersistedTradeMap()
        {
            HasKey(x => x.CorrelationId);

            Property(x => x.TradeId);
            Property(x => x.CurrentState);
        }
    }
}