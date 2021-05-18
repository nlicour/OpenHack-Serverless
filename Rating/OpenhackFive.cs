using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using OpenHack.Entities;
using OpenHack.Service;

namespace OpenHack
{
    public static class Ratings
    {
        [FunctionName("CreateRating")]
        public static async Task<IActionResult> CreateRating(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "Rating",
                collectionName: "Items",
                ConnectionStringSetting = "XXX")]
                IAsyncCollector<RatingType> toDoItemsOut,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            RatingType rating = new RatingType()
            {
                ProductId = data?.productId,
                UserId = data?.userId,
            };

            if (!ApiService.IsValidProduct(rating.ProductId) || !ApiService.IsValidUser(rating.UserId)){
                return new OkObjectResult(rating);
            }

            return new OkObjectResult(rating);
        }
    }
}
