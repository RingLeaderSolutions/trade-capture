using EDF.TradeCapture.Messaging.Events;

namespace EDF.TradeCapture.PomaxPump.Events
{
    public class TradeSentToPomaxPumpEvent : ITradeSentToPomaxPumpEvent
    {
        public string TradeId { get; set; }

        public TradeSentToPomaxPumpEvent(string tradeId)
        {
            TradeId = tradeId;
        }
    }
}