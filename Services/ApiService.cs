using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenHack.Service
{
    public static class ApiService
    {
        public async static Task<bool> IsValidUser(string id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync($"https://serverlessohuser.trafficmanager.net/api/GetUser?userId={id}");
                return result.IsSuccessStatusCode;
            }
        }

        public async static Task<bool> IsValidProduct(string id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync($"https://serverlessohproduct.trafficmanager.net/api/GetProduct?productId={id}");
                return result.IsSuccessStatusCode;
            }
        }
    }
}