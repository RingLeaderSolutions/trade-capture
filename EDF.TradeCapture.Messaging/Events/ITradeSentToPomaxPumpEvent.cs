namespace EDF.TradeCapture.Messaging.Events
{
    public interface ITradeSentToPomaxPumpEvent
    {
        string TradeId { get; set; }
    }
}