using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace xknor.Functions
{
    public static class GameConfigEncoder
    {
        [FunctionName("GameConfigEncoder")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var asd = JsonConvert.DeserializeObject<Model.Config>(requestBody);
            var configBytesArray = System.Text.Encoding.UTF8.GetBytes(requestBody);
            var encodedConfig = System.Convert.ToBase64String(configBytesArray);

            return new OkObjectResult(encodedConfig);
        }
    }
}
