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
        ///     Get the current weather of a specific city by indicating the city, country, language and units (metric or imperial).
        /// </summary>
        /// <param name="city">Name of the city.</param>
        /// <param name="country">Country of the city.</param>
        /// <param name="language">The language of the information returned (example: English - en, Russian - ru, Italian - it, Spanish - sp, Ukrainian - ua, German - de, Portuguese - pt, Romanian - ro, Polish - pl, Finnish - fi, Dutch - nl, French - fr, Bulgarian - bg, Swedish - se, Chinese Traditional - zh_tw, Chinese Simplified - zh_cn, Turkish - tr , Czech - cz, Galician - gl, Vietnamese - vi, Arabic - ar, Macedonian - mk, Slovak - sk).</param>
        /// <param name="units">The units of the date (metric or imperial).</param>
        /// <returns> The weather information.</returns>
        public static SingleResult<WeatherCurrent> GetByCityName(String city, String country, String language, String units)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(city) || String.IsNullOrEmpty(country))
                    return new SingleResult<WeatherCurrent>(null, false, "City and/or Country cannot be empty.");
                var response = ApiClient.GetResponse("/weather?q=" + city + "," + country + "&lang=" + language + "&units=" + units);
                return Deserializer.GetWeatherCurrent(response);
            }
            catch (Exception ex)
            {
                return new SingleResult<WeatherCurrent> { Item = null, Success = false, Message = ex.Message };
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
        ///     Get the current weather of a specific city by indicating its coordinates, language and units (metric or imperial).
        /// </summary>
        /// <param name="lat">Latitud of the city.</param>
        /// <param name="lon">Longitude of the city.</param>
        /// <param name="language">The language of the information returned (example: English - en, Russian - ru, Italian - it, Spanish - sp, Ukrainian - ua, German - de, Portuguese - pt, Romanian - ro, Polish - pl, Finnish - fi, Dutch - nl, French - fr, Bulgarian - bg, Swedish - se, Chinese Traditional - zh_tw, Chinese Simplified - zh_cn, Turkish - tr , Czech - cz, Galician - gl, Vietnamese - vi, Arabic - ar, Macedonian - mk, Slovak - sk).</param>
        /// <param name="units">The units of the date (metric or imperial).</param>
        /// <returns> The weather information.</returns>
        public static SingleResult<WeatherCurrent> GetByCoordinates(double lat, double lon, String language, String units)
        {
            try
            {
                var o = ApiClient.GetResponse("/weather?lat=" + lat + "&lon=" + lon + "&lang=" + language + "&units=" + units);
                return Deserializer.GetWeatherCurrent(o);
            }
            catch (Exception ex)
            {
                return new SingleResult<WeatherCurrent> { Item = null, Success = false, Message = ex.Message };
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

        /// <summary>
        ///     Get the current weather of a specific city by indicating its 'OpenwWeatherMap' identifier, language and units (metric or imperial).
        /// </summary>
        /// <param name="id">City 'OpenwWeatherMap' identifier.</param>
        /// <param name="language">The language of the information returned (example: English - en, Russian - ru, Italian - it, Spanish - sp, Ukrainian - ua, German - de, Portuguese - pt, Romanian - ro, Polish - pl, Finnish - fi, Dutch - nl, French - fr, Bulgarian - bg, Swedish - se, Chinese Traditional - zh_tw, Chinese Simplified - zh_cn, Turkish - tr , Czech - cz, Galician - gl, Vietnamese - vi, Arabic - ar, Macedonian - mk, Slovak - sk).</param>
        /// <param name="units">The units of the date (metric or imperial).</param>
        /// <returns> The weather information.</returns>
        public static SingleResult<WeatherCurrent> GetByCityId(int id, String language, String units)
        {
            try
            {
                if (0 > id)
                    return new SingleResult<WeatherCurrent>(null, false, "City Id must be a positive number.");
                var o = ApiClient.GetResponse("/weather?id=" + id + "&lang=" + language + "&units=" + units);
                return Deserializer.GetWeatherCurrent(o);
            }
            catch (Exception ex)
            {
                return new SingleResult<WeatherCurrent> { Item = null, Success = false, Message = ex.Message };
            }
        }
    }
}
