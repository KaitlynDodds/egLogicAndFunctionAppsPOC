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
    /**
     * TriggerLogicApp
     *      HttpTrigger
     *      POST
     * 
     * Can be used to trigger the egLogicAppPOC
     * 
     */

    public static class TriggerLogicApp
    {
        private static string LogicAppUri = "https://prod-20.westus.logic.azure.com:443/workflows/0bec38eae4234d988a02da6f7591b91a/triggers/manual/paths/invoke?api-version=2017-07-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=eBy1Va28OvE39YqX1G89eI0uXTmvRCCmQiHBVHc46qI";
        [FunctionName("TriggerLogicApp")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("TriggerLogicApp processed a request.");
            var data = await req.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(data); 
            
            /** Make HTTP call to Logic App **/
            log.Info("Making call to logic app");
            using (var client = new HttpClient())
            {
                return await client.PostAsync(LogicAppUri, new StringContent( json.ToString(), Encoding.UTF8, "application/json"));
            }
        }
    }
}
