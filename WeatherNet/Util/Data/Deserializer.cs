#region

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using WeatherNet.Model;

#endregion

namespace WeatherNet.Util.Data
{
    internal class Deserializer
    {
        public static SingleResult<WeatherCurrent> GetWeatherCurrent(JObject response)
        {
            var error = GetServerErrorFromResponse(response);
            if (!String.IsNullOrEmpty(error))
                return new SingleResult<WeatherCurrent>(null, false, error);

            var weatherCurrent = new WeatherCurrent();

            if (response["sys"] != null)
            {
                weatherCurrent.Country = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(response["sys"]["country"])));
            }

            if (response["weather"] != null)
            {
                weatherCurrent.Title = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(response["weather"][0]["main"])));
                weatherCurrent.Description = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(response["weather"][0]["description"])));
                weatherCurrent.Icon = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(response["weather"][0]["icon"])));
            }

            if (response["main"] != null)
            {
                weatherCurrent.Temp = Convert.ToDouble(response["main"]["temp"]);
                weatherCurrent.TempMax = Convert.ToDouble(response["main"]["temp_max"]);
                weatherCurrent.TempMin = Convert.ToDouble(response["main"]["temp_min"]);
                weatherCurrent.Humidity = Convert.ToDouble(response["main"]["humidity"]);
            }

            if (response["wind"] != null)
            {
                weatherCurrent.WindSpeed = Convert.ToDouble(response["wind"]["speed"]);
            }

            weatherCurrent.Date = DateTime.UtcNow;
            weatherCurrent.City = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(response["name"])));
            weatherCurrent.CityId = Convert.ToInt32(response["id"]);

            return new SingleResult<WeatherCurrent>(weatherCurrent, true, TimeHelper.MessageSuccess);
        }

        public static Result<WeatherForecast> GetWeatherForecast(JObject response)
        {
            var error = GetServerErrorFromResponse(response);
            if (!String.IsNullOrEmpty(error))
                return new Result<WeatherForecast>(null, false, error);


            var weatherForecasts = new List<WeatherForecast>();

            var responseItems = JArray.Parse(response["list"].ToString());
            foreach (var item in responseItems)
            {
                var weatherForecast = new WeatherForecast();
                if (response["city"] != null)
                {
                    weatherForecast.City = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(response["city"]["name"])));
                    weatherForecast.Country = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(response["city"]["country"])));
                    weatherForecast.CityId = Convert.ToInt32(response["city"]["id"]);
                }

                if (item["weather"] != null)
                {
                    weatherForecast.Title = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(item["weather"][0]["main"])));
                    weatherForecast.Description = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(item["weather"][0]["description"])));
                    weatherForecast.Icon = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(item["weather"][0]["icon"])));
                }

                if (item["main"] != null)
                {
                    weatherForecast.Temp = Convert.ToDouble(item["main"]["temp"]);
                    weatherForecast.TempMax = Convert.ToDouble(item["main"]["temp_max"]);
                    weatherForecast.TempMin = Convert.ToDouble(item["main"]["temp_min"]);
                    weatherForecast.Humidity = Convert.ToDouble(item["main"]["humidity"]);
                }

                if (item["wind"] != null)
                {
                    weatherForecast.WindSpeed = Convert.ToDouble(item["wind"]["speed"]);
                }

                if (item["clouds"] != null)
                {
                    weatherForecast.Clouds = Convert.ToDouble(item["clouds"]["all"]);
                }
                weatherForecast.Date = Convert.ToDateTime(item["dt_txt"]);
                weatherForecast.DateUnixFormat = Convert.ToInt32(item["dt"]);

                weatherForecasts.Add(weatherForecast);
            }

            return new Result<WeatherForecast>(weatherForecasts, true, TimeHelper.MessageSuccess);
        }

        public static Result<WeatherDaily> GetWeatherDaily(JObject response)
        {
            var error = GetServerErrorFromResponse(response);
            if (!String.IsNullOrEmpty(error))
                return new Result<WeatherDaily>(null, false, error);

            var weatherDailies = new List<WeatherDaily>();

            var responseItems = JArray.Parse(response["list"].ToString());
            foreach (var item in responseItems)
            {
                var weatherDaily = new WeatherDaily();
                if (response["city"] != null)
                {
                    weatherDaily.City = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(response["city"]["name"])));
                    weatherDaily.Country = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(response["city"]["country"])));
                    weatherDaily.CityId = Convert.ToInt32(response["city"]["id"]);
                }
                if (item["weather"] != null)
                {
                    weatherDaily.Title = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(item["weather"][0]["main"])));
                    weatherDaily.Description = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(item["weather"][0]["description"])));
                    weatherDaily.Icon = Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(item["weather"][0]["icon"])));
                }
                if (item["temp"] != null)
                {
                    weatherDaily.Temp = Convert.ToDouble(item["temp"]["day"]);
                    weatherDaily.TempMax = Convert.ToDouble(item["temp"]["max"]);
                    weatherDaily.TempMin = Convert.ToDouble(item["temp"]["min"]);
                    weatherDaily.TempMorning = Convert.ToDouble(item["temp"]["morn"]);
                    weatherDaily.TempEvening = Convert.ToDouble(item["temp"]["eve"]);

                    weatherDaily.TempNight = Convert.ToDouble(item["temp"]["night"]);
                }

                weatherDaily.Humidity = Convert.ToDouble(item["humidity"]);
                weatherDaily.WindSpeed = Convert.ToDouble(item["speed"]);
                weatherDaily.Clouds = Convert.ToDouble(item["clouds"]);
                weatherDaily.Pressure = Convert.ToDouble(item["pressure"]);
                weatherDaily.Rain = Convert.ToDouble(item["rain"]);
                weatherDaily.DateUnixFormat = Convert.ToInt32(item["dt"]);
                weatherDaily.Date = TimeHelper.ToDateTime(Convert.ToInt32(item["dt"]));

                weatherDailies.Add(weatherDaily);
            }

            return new Result<WeatherDaily>(weatherDailies, true, TimeHelper.MessageSuccess);
        }

        public static string GetServerErrorFromResponse(JObject response)
        {
            if (response["cod"].ToString() == "200")
                return null;

            var errorMessage = "Server error " + response["cod"];
            if (!String.IsNullOrEmpty(response["message"].ToString()))
                errorMessage += ". " + response["message"];
            return errorMessage;
        }
    }
}
