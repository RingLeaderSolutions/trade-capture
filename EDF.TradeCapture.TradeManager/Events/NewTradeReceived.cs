using EDF.TradeCapture.Messaging.Events;

namespace EDF.TradeCapture.ValidationService.Events
{
    public class NewTradeReceived : INewTradeReceived
    {
        public string TradeId { get; set; }

        public NewTradeReceived(string tradeId)
        {
            TradeId = tradeId;
        }
    }
}