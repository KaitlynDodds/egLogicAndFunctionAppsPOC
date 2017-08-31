using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace egLogicAndFunctionAppsPOC
{
    public static class PayWithPaypal
    {
        [FunctionName("PayWithPaypal")]
        public static async Task<object> Run([HttpTrigger(WebHookType = "genericJson")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info($"PayWithPaypal was triggered!");

            /** Parse JSON object **/
            string jsonContent = await req.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(jsonContent);

            /** Check Data **/
            if (data == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    error = "Invalid Data"
                });
            }

            /** Data should be good, proceed with payment... **/
            log.Info("Paying with Paypal");
            return req.CreateResponse(HttpStatusCode.OK, new
            {
                greeting = $"Paying with Paypal for order {data.purchaseID}"
            });
        }
    }
}
