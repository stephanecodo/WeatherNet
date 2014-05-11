#region

using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json.Linq;

#endregion

namespace WeatherNet.Util.Api
{
    internal class ApiClient
    {
        public static string API_URL = "http://api.openweathermap.org/data/2.5";

        public static JObject GetResponse(String url)
        {
            using (var client = new WebClient())
            {
                Trace.WriteLine("<HTTP - GET - " + url + " >");
                var response = client.DownloadString(API_URL + url);
                var parsedResponse = JObject.Parse(response);
                return parsedResponse;
            }
        }
    }
}