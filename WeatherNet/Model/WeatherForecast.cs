#region

using System;
using WeatherNet.Model;

#endregion

namespace WeatherNet.Model
{
    /// <summary>
    ///     WeatherForecast result type.
    /// </summary>
    public class WeatherForecast : Weather
    {
        /// <summary>
        ///     Time of data receiving in unixtime GMT.
        /// </summary>
        public int DateUnixFormat { get; set; }

        /// <summary>
        ///     Cloudiness in %
        /// </summary>
        public Double Clouds { get; set; }
    }
}