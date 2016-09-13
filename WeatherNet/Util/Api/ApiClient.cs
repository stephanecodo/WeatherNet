#region

using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json.Linq;

#endregion

namespace WeatherNet.Util.Api
{
    public class ApiClient
    {
        public static string API_URL = "http://api.openweathermap.org/data/2.5";
        public static void ProvideApiKey(string apiKey)
        {
            api_key = apiKey;
        }
        static string api_key;
        public static string API_KEY 
        { 
            get
            {
                if (string.IsNullOrEmpty(api_key))
                {
                    var key = ConfigurationManager.AppSettings["APIKey"];
                    if (string.IsNullOrEmpty(key))
                        throw new Exception("API key not found");

                    api_key = key;
                }

                return api_key;
            }
        }

        public static JObject GetResponse(String url)
        {
            using (var client = new WebClient())
            {
                Trace.WriteLine("<HTTP - GET - " + url + " >");
                var response = client.DownloadString(string.Format("{0}{1}&appid={2}", API_URL, url, API_KEY));
                var parsedResponse = JObject.Parse(response);
                return parsedResponse;
            }
        }
    }
}