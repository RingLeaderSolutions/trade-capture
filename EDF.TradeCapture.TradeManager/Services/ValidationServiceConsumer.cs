using System.Threading.Tasks;
using EDF.TradeCapture.Messaging.Commands;
using EDF.TradeCapture.Messaging.Events;
using EDF.TradeCapture.ValidationService.Events;
using MassTransit;
using Serilog;

namespace EDF.TradeCapture.ValidationService.Services
{
    public class ValidationServiceConsumer : IConsumer<IValidationRequestedCommand>
    {
        public async Task Consume(ConsumeContext<IValidationRequestedCommand> context)
        {
            var tradeId = context.Message.TradeId;
            Log.Information("[Validation Service] Validating data for Trade: TradeId=[{TradeId}]", tradeId);
            await context.Publish<IValidationCompletedEvent>(new ValidationCompletedEvent(tradeId));
        }
    }
}