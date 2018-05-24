using EDF.TradeCapture.Messaging.Commands;

namespace EDF.TradeCapture.StateSaga.Commands
{
    public class ValidationRequestedCommand : IValidationRequestedCommand
    {
        public string TradeId { get; }

        public ValidationRequestedCommand(string tradeId)
        {
            this.TradeId = tradeId;
        }
    }
}