using Autofac;
using EDF.TradeCapture.ValidationService.Services;

namespace EDF.TradeCapture.ValidationService
{
    public class TradeManagerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TradeCaptureService>();
        }
    }
}