using Autofac;

namespace EDF.TradeCapture.StateSaga
{
    public class SagaModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SagaStateService>();
        }
    }
}