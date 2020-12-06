using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using xknor.Functions.Model;
using System.Data.SqlClient;
using Dapper;

namespace xknor.Functions
{
    public static class GameDataCollector
    {
        [FunctionName("GameDataCollector")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            GameData data = JsonConvert.DeserializeObject<GameData>(requestBody);

            var connectionString = System.Environment.GetEnvironmentVariable($"ConnectionString");

            if (string.IsNullOrEmpty(connectionString))
            {
                return new Microsoft.AspNetCore.Mvc.BadRequestObjectResult("Connection string is empty");
            }
            
            try 
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    
                    con.Execute($@"INSERT INTO GameData VALUES 
                        ('{data.TimeAdded.ToString("s")}', '{data.Username}', '{data.GameConfig}', {data.GainedPoints}, {data.LongestPerfectStreak},
                        {data.CorrectActions}, {data.TotalActions}, {data.CorrectSequences}, {data.TotalSequences}, {data.TimeSpent},
                        {data.TimeLimit}, {data.SequencesButton}, {data.CorrectSequencesButton}, {data.SequencesTimeLimit}, {data.CorrectSequencesTimeLimit});");
                    
                    con.Close();
                }
                
                var responseMessage = "Game data saved successfully";

                return new OkObjectResult(responseMessage);
            }
            catch (Exception e)
            {
                return new ObjectResult(e);
            }
        }
    }
}
