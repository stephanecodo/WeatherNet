#region

using System;
using WeatherNet.Model;
using WeatherNet.Util.Api;
using WeatherNet.Util.Data;

#endregion

namespace WeatherNet
{
    public class Current
    {
        /// <summary>
        ///     Get the current weather of a specific city by indicating the city and country names.
        /// </summary>
        /// <param name="city">Name of the city.</param>
        /// <param name="country">Country of the city.</param>
        /// <returns> The weather information.</returns>
        public static SingleResult<WeatherCurrent> GetByCityName(String city, String country)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(city) || String.IsNullOrEmpty(country))
                    return new SingleResult<WeatherCurrent>(null, false, "City and/or Country cannot be empty.");
                var response = ApiClient.GetResponse("/weather?q=" + city + "," + country);
                return Deserializer.GetWeatherCurrent(response);
            }
            catch (Exception ex)
            {
                return new SingleResult<WeatherCurrent> {Item = null, Success = false, Message = ex.Message};
            }
        }

        /// <summary>
        ///     Get the current weather of a specific city by indicating its coordinates.
        /// </summary>
        /// <param name="lat">Latitud of the city.</param>
        /// <param name="lon">Longitude of the city.</param>
        /// <returns> The weather information.</returns>
        public static SingleResult<WeatherCurrent> GetByCoordinates(double lat, double lon)
        {
            try
            {
                var o = ApiClient.GetResponse("/weather?lat=" + lat + "&lon=" + lon);
                return Deserializer.GetWeatherCurrent(o);
            }
            catch (Exception ex)
            {
                return new SingleResult<WeatherCurrent> {Item = null, Success = false, Message = ex.Message};
            }
        }

        /// <summary>
        ///     Get the current weather of a specific city by indicating its 'OpenwWeatherMap' identifier.
        /// </summary>
        /// <param name="id">City 'OpenwWeatherMap' identifier.</param>
        /// <returns> The weather information.</returns>
        public static SingleResult<WeatherCurrent> GetByCityId(int id)
        {
            try
            {
                if (0 > id)
                    return new SingleResult<WeatherCurrent>(null, false, "City Id must be a positive number.");
                var o = ApiClient.GetResponse("/weather?id=" + id);
                return Deserializer.GetWeatherCurrent(o);
            }
            catch (Exception ex)
            {
                return new SingleResult<WeatherCurrent> {Item = null, Success = false, Message = ex.Message};
            }
        }
    }
}