using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Serilog;
using Topshelf;

namespace EDF.TradeCapture.ValidationService.Services
{
    internal class TradeCaptureService: ServiceControl
    {
        protected IDisposable SelfHostedApp { get; set; }

        protected string Port { get; } = ConfigurationManager.AppSettings["port"];

        protected string BaseAddress { get; } = ConfigurationManager.AppSettings["baseAddress"];


        public bool Start(HostControl hostControl)
        {
            Task.Factory.StartNew(
                () =>
                {
                    string baseAddress = $"http://{BaseAddress}:{Port}/";

                    SelfHostedApp = WebApp.Start<Startup>(baseAddress);
                    
                    Log.Information("ValidationService Web API started at [{baseAddress}]", baseAddress);
                });

            return true;
        }


        public bool Stop(HostControl hostControl)
        {
            Task.Factory.StartNew(
                () =>
                {
                    SelfHostedApp.Dispose();
                });

            return true;

        }
    }
}