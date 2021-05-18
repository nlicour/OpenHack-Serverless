using System;
using System.Net.Http;

namespace OpenHack.Service
{
    public static class ApiService
    {
        public static bool IsValidUser(string id){
            HttpClient httpClient = new HttpClient();
            return true;
        }

        public static bool IsValidProduct(string id){
            return true;
        }
    }
}