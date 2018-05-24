using System.Threading.Tasks;
using EDF.TradeCapture.GeSwapPump.Events;
using EDF.TradeCapture.Messaging.Commands;
using EDF.TradeCapture.Messaging.Events;
using MassTransit;
using Serilog;

namespace EDF.TradeCapture.GeSwapPump
{
    public class GeSwapPumpConsumer : IConsumer<ISendTradeToGeSwapPumpCommand>
    {
        public async Task Consume(ConsumeContext<ISendTradeToGeSwapPumpCommand> context)
        {
            var tradeId = context.Message.TradeId;
            Log.Information("[GE Swap Pump] Trade received: TradeId=[{TradeId}]", tradeId);
            await context.Publish<ITradeSentToGeSwapPumpEvent>(new TradeSentToGeSwapPumpEvent(tradeId));
        }
    }
}