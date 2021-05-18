using System;
using System.Net.Http;

namespace OpenHack.Service
{
    public static class ApiService
    {
        // ne pas juger le .Result
        public static bool IsValidUser(string id){
            HttpClient httpClient = new HttpClient();
            var result = httpClient.GetAsync($"https://serverlessohproduct.trafficmanager.net/api/GetUser?userId={id}").Result;
            return result.IsSuccessStatusCode;
        }

        public static bool IsValidProduct(string id){
            HttpClient httpClient = new HttpClient();
            var result = httpClient.GetAsync($"https://serverlessohproduct.trafficmanager.net/api/GetProduct?productId={id}").Result;
            return result.IsSuccessStatusCode;
        }
    }
}