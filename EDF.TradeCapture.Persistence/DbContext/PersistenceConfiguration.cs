using System.Configuration;

namespace EDF.TradeCapture.Persistence.DbContext
{
    public class PersistenceConfiguration
    {
        public static string ConnectionString { get; } = ConfigurationManager.AppSettings["sqlConnectionString"];
    }
}