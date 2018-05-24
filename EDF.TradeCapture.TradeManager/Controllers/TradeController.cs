using System;
using System.Threading.Tasks;
using System.Web.Http;
using EDF.TradeCapture.Messaging.Events;
using EDF.TradeCapture.Messaging.MassTransit;
using EDF.TradeCapture.ValidationService.Events;
using MassTransit;
using Serilog;

namespace EDF.TradeCapture.ValidationService.Controllers
{
    [RoutePrefix("api/trade")]
    public class TradeController : ApiController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public TradeController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [Route]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [Route]
        [HttpPost]
        public async Task<IHttpActionResult> NewTradeReceived([FromBody]NewTradeReceived newTradeReceived)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(RabbitMqConnectionInformation.TradeSagaEndpoint));
            Log.Information("[Validation Service] Received new Trade: TradeId=[{TradeId}]", newTradeReceived.TradeId);
            await endpoint.Send<INewTradeReceived>(new NewTradeReceived(newTradeReceived.TradeId));

            return Ok();
        }
    }
}