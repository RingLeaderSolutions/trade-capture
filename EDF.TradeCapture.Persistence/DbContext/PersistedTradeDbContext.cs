using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EDF.TradeCapture.Persistence.Model;
using MassTransit.EntityFrameworkIntegration;

namespace EDF.TradeCapture.Persistence.DbContext
{
    public class PersistedTradeDbContext : SagaDbContext<PersistedTrade, PersistedTradeMap>,
        IPersistedTradeDbContext
    {
        public PersistedTradeDbContext() : base(PersistenceConfiguration.ConnectionString)
        {
        }

        public DbSet<PersistedTrade> Quotes { get; set; }


        public async Task<List<PersistedTrade>> GetOutstandingTrades()
        {
            return await RetrieveTrades()
                .Where(q => q.CurrentState != "Completed")
                .ToListAsync();
        }


        private IQueryable<PersistedTrade> RetrieveTrades()
        {
            // AsNoTracking disables EF's internal caching of this query, which is safe to do as this is 
            // only ever requested from our read-only layer.
            return Quotes.AsQueryable()
                .AsNoTracking();
        }
    }
}
