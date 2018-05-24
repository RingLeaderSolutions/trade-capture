namespace EDF.TradeCapture.Messaging.Events
{
    public interface INewTradeReceived
    {
        string TradeId { get; set; }
    }
}