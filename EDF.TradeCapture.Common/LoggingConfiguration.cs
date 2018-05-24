using System.Configuration;
using EDF.TradeCapture.Common.Logging;
using Serilog;
using Serilog.Core;

namespace EDF.TradeCapture.Common
{
    public static class LoggingConfiguration
    {
        public static string Path { get; } = ConfigurationManager.AppSettings["log_path"];

        public static string SeqUrl { get; } = ConfigurationManager.AppSettings["seq_url"];


        public static Logger ConfigureLogger()
        {
            return ConfigureLogger(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }


        private static Logger ConfigureLogger(string serviceName)
        {
            var configuration = new LoggerConfiguration()
                .Enrich.With(new ServiceNameEnricher(serviceName))
                .WriteTo.Console()
                .WriteTo.RollingFile($"{Path}{serviceName}" + "-{Date}.log")
                .WriteTo.Seq(SeqUrl)
                .CreateLogger();
            Log.Logger = configuration;

            return configuration;
        }
    }
}