
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace SampleFunctionapp
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async System.Threading.Tasks.Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed sucessfully"
                : $"Hellossss, {name}. This HTTP triggered function executed succesfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
