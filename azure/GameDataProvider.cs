using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using xknor.Functions.Model;
using System.Data.SqlClient;
using System.Collections.Generic;
using Dapper;

namespace xknor.Functions
{
    public static class GameDataProvider
    {
        [FunctionName("GameDataProvider")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var connectionString = System.Environment.GetEnvironmentVariable($"ConnectionString");

            if (string.IsNullOrEmpty(connectionString))
            {
                return new Microsoft.AspNetCore.Mvc.BadRequestObjectResult("Connection string is empty");
            }
            
            IEnumerable<GameData> gameData;
            try 
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();

                    gameData = await con.QueryAsync<GameData>("SELECT * FROM GameData ORDER BY DataId;");                    
                    
                    con.Close();
                }
                
                var header = "DataId,Čas přidání,Přezdívka,Konfigurace hry,Získané body,Nejdelší bezchybný řetězec,"
                    + "Správné úkony,Celkové úkony,Správné sekvence,Celkové sekvence,Čas hry,Časový limit,"
                    + "Počet sekvencí odevzdaných tlačítkem, Počet správných sekvencí odevzdaných tlačítkem,"
                    + "Počet sekvencí odevzdaných vypršením časového limitu,"
                    + "Počet správných sekvencí odevzdaných vypršením časového limitu\n";
                var lines = gameData.Select(x => $"{x.DataId},{x.TimeAdded.ToString("dd/MM/yyyy HH:mm:ss")},{x.Username},{x.GameConfig.Replace(",", ";")},"
                    + $"{x.GainedPoints},{x.LongestPerfectStreak},{x.CorrectActions},{x.TotalActions},{x.CorrectSequences},{x.TotalSequences},"
                    + $"{x.TimeSpent},{x.TimeLimit},{x.SequencesButton},{x.CorrectSequencesButton},{x.SequencesTimeLimit},"
                    + $"{x.CorrectSequencesTimeLimit}").Aggregate((a, b) => $"{a}\n{b}");
                var bytes = Encoding.UTF8.GetBytes(header + lines);

                var stream = new MemoryStream(bytes);
                var res = new FileStreamResult(stream, "text/csv; charset=UTF-8");
                res.FileDownloadName = "results.csv";

                return res;
            }
            catch (Exception e)
            {
                return new ObjectResult(e);
            }
        }
    }
}
