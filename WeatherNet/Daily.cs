#region

using System;
using WeatherNet.Model;
using WeatherNet.Util.Api;
using WeatherNet.Util.Data;

#endregion

namespace WeatherNet
{
    public class Daily
    {
        /// <summary>
        ///     Get the daily forecast for a specific city by indicating its 'OpenwWeatherMap' identifier.
        /// </summary>
        /// <param name="id">City 'OpenwWeatherMap' identifier.</param>
        /// <param name="days">Number of days we want the forecast for (from 0 to 14).</param>
        /// <returns>The forecast information.</returns>
        public static Result<WeatherDaily> GetByCityId(int id, int days)
        {
            try
            {
                if (0 > days || days > 16)
                    return new Result<WeatherDaily>(null, false, "Days must be a value between 1 and 16.");
                if (0 > id)
                    return new Result<WeatherDaily>(null, false, "City Id must be a positive number.");
                var response = ApiClient.GetResponse("/forecast/daily?id=" + id + "&cnt=" + days);
                return Deserializer.GetWeatherDaily(response);
            }
            catch (Exception ex)
            {
                return new Result<WeatherDaily> {Items = null, Success = false, Message = ex.Message};
            }
        }

        /// <summary>
        ///     Get the daily forecast for a specific city by indicating the city and country names.
        /// </summary>
        /// <param name="city">Name of the city.</param>
        /// <param name="country">Country of the city.</param>
        /// <param name="days">Number of days we want the forecast for (from 0 to 14).</param>
        /// <returns>The forecast information.</returns>
        public static Result<WeatherDaily> GetByCityName(String city, String country, int days)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(city) || String.IsNullOrEmpty(country))
                    return new Result<WeatherDaily>(null, false, "City and/or Country cannot be empty.");
                if (0 > days || days > 16)
                    return new Result<WeatherDaily>(null, false, "Days must be a value between 1 and 16.");

                var response =
                    ApiClient.GetResponse("/forecast/daily?q=" + city + "," + country + "&cnt=" + days);
                return Deserializer.GetWeatherDaily(response);
            }
            catch (Exception ex)
            {
                return new Result<WeatherDaily> {Items = null, Success = false, Message = ex.Message};
            }
        }

        /// <summary>
        ///     Get the daily forecast for a specific city by indicating its coordinates.
        /// </summary>
        /// <param name="lat">Latitud of the city.</param>
        /// <param name="lon">Longitude of the city.</param>
        /// <param name="days">Number of days we want the forecast for (from 0 to 14).</param>
        /// <returns>The forecast information.</returns>
        public static Result<WeatherDaily> GetByCoordinates(double lat, double lon, int days)
        {
            try
            {
                if (0 > days || days > 16)
                    return new Result<WeatherDaily>(null, false, "Days must be a value between 1 and 16.");
                var response =
                    ApiClient.GetResponse("/forecast/daily?lat=" + lat + "&lon=" + lon + "&cnt=" + days);
                return Deserializer.GetWeatherDaily(response);
            }
            catch (Exception ex)
            {
                return new Result<WeatherDaily> {Items = null, Success = false, Message = ex.Message};
            }
        }
    }
}
