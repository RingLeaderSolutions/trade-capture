using System.Configuration;

namespace EDF.TradeCapture.StateSaga.StateMachine
{
    public class StateSagaConfiguration
    {
        public static string ValidationDataEndpoint { get; } = ConfigurationManager.AppSettings["rabbitmq_validationDataEndpoint"];

        public static string PomaxPumpEndpoint { get; } = ConfigurationManager.AppSettings["rabbitmq_pomaxPumpEndpoint"];

        public static string GeSwapPumpEndpoint { get; } = ConfigurationManager.AppSettings["rabbitmq_geSwapPumpEndpoint"];
    }
}