using System.Collections.Generic;
using System.Threading.Tasks;
using EDF.TradeCapture.Persistence.Model;

namespace EDF.TradeCapture.Persistence.DbContext
{
    public interface IPersistedTradeDbContext
    {
        Task<List<PersistedTrade>> GetOutstandingTrades();
    }
}