using EDF.TradeCapture.Messaging.Events;

namespace EDF.TradeCapture.GeSwapPump.Events
{
    public class TradeSentToGeSwapPumpEvent : ITradeSentToGeSwapPumpEvent
    {
        public string TradeId { get; set; }

        public TradeSentToGeSwapPumpEvent(string tradeId)
        {
            TradeId = tradeId;
        }
    }
}