namespace EDF.TradeCapture.Messaging.Commands
{
    public interface IValidationRequestedCommand
    {
        string TradeId { get; }
    }
}