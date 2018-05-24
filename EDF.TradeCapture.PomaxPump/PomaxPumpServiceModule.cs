using Autofac;

namespace EDF.TradeCapture.PomaxPump
{
    internal class PomaxPumpServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PomaxPumpService>();
        }
    }
}