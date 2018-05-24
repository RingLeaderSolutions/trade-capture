using Autofac;
using TradeManager.Repositories;
using TradeManager.Services;

namespace TradeManager
{
    public class TradeManagerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TradeService>();
            builder.RegisterType<TradeRepository>();
        }
    }
}