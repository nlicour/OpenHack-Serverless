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
                databaseName: "Ratings",
                collectionName: "Rating",
                ConnectionStringSetting = "connectionStringSetting")]
                IAsyncCollector<RatingType> ratingsCollection,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            RatingType rating = JsonConvert.DeserializeObject<RatingType>(requestBody);

            if (!(await ApiService.IsValidUser(rating.userId)))
            {
                return new BadRequestObjectResult($"Invalid userId {rating.userId}");
            }

            if (!await ApiService.IsValidProduct(rating.productId))
            {
                return new BadRequestObjectResult($"Invalid productId {rating.userId}");
            }

            if (rating.rating < 0 || rating.rating > 5)
            {
                return new BadRequestObjectResult($"Invalid rating : {rating.rating}");
            }

            rating.id = Guid.NewGuid().ToString();
            rating.timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();

            await ratingsCollection.AddAsync(rating);

            return new OkObjectResult(rating);
        }
    }
}
