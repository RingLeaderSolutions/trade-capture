namespace EDF.TradeCapture.Messaging.Commands
{
    public interface ISendTradeToPomaxPumpCommand
    {
        string TradeId { get; }
    }
}