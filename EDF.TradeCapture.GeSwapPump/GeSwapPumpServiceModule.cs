using Autofac;

namespace EDF.TradeCapture.GeSwapPump
{
    internal class GeSwapPumpServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GeSwapPumpService>();
        }
    }
}