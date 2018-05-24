namespace EDF.TradeCapture.Messaging.Commands
{
    public interface ISendTradeToGeSwapPumpCommand
    {
        string TradeId { get; }
    }
}