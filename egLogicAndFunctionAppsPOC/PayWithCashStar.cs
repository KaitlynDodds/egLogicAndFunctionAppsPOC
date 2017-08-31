using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace egLogicAndFunctionAppsPOC
{
    public static class PayWithCashStar
    {
        [FunctionName("PayWithCashStar")]
        public static async Task<object> Run([HttpTrigger(WebHookType = "genericJson")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info($"PayWithCashStar was triggered!");

            string jsonContent = await req.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(jsonContent);

            if (data == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    error = "invalid data"
                });
            }

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                greeting = $"Paying with CashStar for order {data.purchaseID}"
            });
        }
    }
}
