using System;
using System.Linq;
using System.Threading;
using TradeManager.Repositories;

namespace TradeManager.Services
{
    internal class TradeService
    {
        private readonly TradeRepository _tradeRepository;

        public TradeService(TradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }


        public void ShowTrades()
        {
            var trades = _tradeRepository.GetTrades();
            Console.WriteLine($"The following trades are ready for acceptance: {string.Join(", ", trades)}");
        }


        public bool AcceptTrade(string tradeName)
        {
            if (!IsTradeValid(tradeName)) return false;

            EnrichTrade(tradeName);

            RequestPrice(tradeName);

            AddTradeToRms(tradeName);

            Console.WriteLine($"Trade '{tradeName}' is now been accepted.");
            return true;
        }


        private bool IsTradeValid(string tradeName)
        {
            var trade = _tradeRepository.GetTrades().SingleOrDefault(t => t == tradeName);

            if (trade != null) return true;

            Console.WriteLine($"Trade '{tradeName}' not found.");
            return false;

        }


        private void EnrichTrade(string tradeName)
        {
            Console.WriteLine($" - Looking for '{tradeName}' CRM.");
            Thread.Sleep(1000);
            Console.WriteLine($" - Trade '{tradeName}' has been found in Salesforce.");
        }


        private void RequestPrice(string tradeName)
        {
            Console.WriteLine($" - Price for trade '{tradeName}' has been requested.");
            Thread.Sleep(2000);
            Console.WriteLine($" - Price for trade '{tradeName}' received.");
        }


        private void AddTradeToRms(string tradeName)
        {
            Console.WriteLine($" - Sending '{tradeName}' to Risk Management System.");
            Thread.Sleep(1000);
            Console.WriteLine($" - Trade '{tradeName}' pushed to Risk Management System.");
        }
    }
}