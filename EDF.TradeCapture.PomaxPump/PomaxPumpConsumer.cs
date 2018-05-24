using System.Threading.Tasks;
using EDF.TradeCapture.Messaging.Commands;
using EDF.TradeCapture.Messaging.Events;
using EDF.TradeCapture.PomaxPump.Events;
using MassTransit;
using Serilog;

namespace EDF.TradeCapture.PomaxPump
{
    public class PomaxPumpConsumer : IConsumer<ISendTradeToPomaxPumpCommand>
    {
        public async Task Consume(ConsumeContext<ISendTradeToPomaxPumpCommand> context)
        {
            var tradeId = context.Message.TradeId;
            Log.Information("[Pomax Pump] Trade received: TradeId=[{TradeId}]", tradeId);
            await context.Publish<ITradeSentToPomaxPumpEvent>(new TradeSentToPomaxPumpEvent(tradeId));
        }
    }
}