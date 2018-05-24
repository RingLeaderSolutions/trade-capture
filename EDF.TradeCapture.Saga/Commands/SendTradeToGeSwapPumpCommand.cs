using EDF.TradeCapture.Messaging.Commands;

namespace EDF.TradeCapture.StateSaga.Commands
{
    public class SendTradeToGeSwapPumpCommand : ISendTradeToGeSwapPumpCommand
    {
        public string TradeId { get; }

        public SendTradeToGeSwapPumpCommand(string tradeId)
        {
            this.TradeId = tradeId;
        }
    }
}