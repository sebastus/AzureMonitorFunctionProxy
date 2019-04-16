using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionProxy
{
    public static class Proxy
    {
        [FunctionName("Proxy")]
        public static async Task<IActionResult> Run(
            [FromBody, HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "proxy")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            return requestBody != null
                ? (ActionResult)new OkObjectResult($"{requestBody}")
                : new BadRequestObjectResult("No request body");
        }
    }
}
