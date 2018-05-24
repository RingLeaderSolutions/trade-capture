using System.Configuration;

namespace EDF.TradeCapture.Messaging.MassTransit
{
    public static class RabbitMqConnectionInformation
    {
        public static string Uri { get; } = ConfigurationManager.AppSettings["rabbitmq_uri"];

        public static string Username { get; } = ConfigurationManager.AppSettings["rabbitmq_username"];

        public static string Password { get; } = ConfigurationManager.AppSettings["rabbitmq_password"];

        public static string TradeSagaEndpoint { get; } = ConfigurationManager.AppSettings["rabbitmq_tradeSagaUri"];
    }
}