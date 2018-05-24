namespace EDF.TradeCapture.Messaging.Events
{
    public interface ITradeSentToGeSwapPumpEvent
    {
        string TradeId { get; set; }
    }
}