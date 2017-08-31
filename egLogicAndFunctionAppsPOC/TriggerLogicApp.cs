using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json.Linq;
using System.Text;

namespace egLogicAndFunctionAppsPOC
{
    public static class TriggerLogicApp
    {
        private static string LogicAppUri = "https://prod-62.westus.logic.azure.com:443/workflows/ca3220c21d014cc3be225c5cdc0fc48b/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=pQPSZ-vYLZlVnO0UwqmmVf6Zd_XHCTXlJiCbm12Ei6o";

        [FunctionName("TriggerLogicApp")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("TriggerLogicApp processed a request.");

            /** Data to be sent to the logic app **/
            string content =
            @"{
                'customerID' : '149914cd-1ef1-4540-8f6f-0f25a73ba154',
                'purchaseID' : 'bca34018b3214a1e85b079090fa770b1',
                'timestamp' : '2017-08-31 16:29:53 UTC',
                'paymentMethod' : 'CashStar'
            }";
            /** Convert to JSON **/
            JObject json = JObject.Parse(content);

            /** Make HTTP call to Logic App **/
            using (var client = new HttpClient())
            {
                return await client.PostAsync(LogicAppUri, new StringContent( json.ToString(), Encoding.UTF8, "application/json"));
            }
        }
    }
}
