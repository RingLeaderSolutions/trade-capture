using EDF.TradeCapture.Messaging.Commands;

namespace EDF.TradeCapture.StateSaga.Commands
{
    public class SendTradeToPomaxPumpCommand : ISendTradeToPomaxPumpCommand
    {
        public string TradeId { get; }

        public SendTradeToPomaxPumpCommand(string tradeId)
        {
            this.TradeId = tradeId;
        }
    }
}