using EDF.TradeCapture.Messaging.Events;

namespace EDF.TradeCapture.ValidationService.Events
{
    public class ValidationCompletedEvent : IValidationCompletedEvent
    {
        public string TradeId { get; set; }

        public ValidationCompletedEvent(string tradeId)
        {
            TradeId = tradeId;
        }
    }
}