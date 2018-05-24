namespace EDF.TradeCapture.Messaging.Events
{
    public interface IValidationCompletedEvent
    {
        string TradeId { get; set; }
    }
}