using System;
using System.Configuration;
using NUnit.Framework;
using WeatherNet;

namespace WeatherNetTest
{
    public class WeatherNetTest
    {
        [SetUp]
        public void Setup()
        {
            ConfigurationManager.AppSettings.Set("APIKey", "api-key");
        }
        
        [Test]
        public void GetCurrentByCityNameTest()
        {
            //Does Not Exist
            var result = Current.GetByCityName("12345325231432", "32412342134231");
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Item);


            //Exist
            result = Current.GetByCityName("Dublin", "Ireland");
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Item);
        }

        [Test]
        public void GetCurrentByCityIdTest()
        {
            //Does not exist
            var result = Current.GetByCityId(1111111);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Item);

            //Exist
            result = Current.GetByCityId(2964574);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Item);
        }

        [Test]
        public void GetCurrentByCityCoordinatesTest()
        {
            //Does Not Exist
            var result = Current.GetByCoordinates(-1984453.363665, -1984453.363665);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Item);

            //Exist
            result = Current.GetByCoordinates(53.363665, -6.255541);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Item);
        }


        [Test]
        public void GetForecastByCityNameTest()
        {
            //Does not exist
            var result = Forecast.GetByCityName("12345325231432", "32412342134231");
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Items);

            //Exist
            result = Forecast.GetByCityName("Dublin", "Ireland");
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Items);
            Assert.IsTrue(result.Items.Count > 0);
            Assert.IsNotNull(result.Items[0]);
        }

        [Test]
        public void GetForecastByCityIdTest()
        {
            //Does not exist
            var result = Forecast.GetByCityId(-2964574);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Items);

            //Exist
            result = Forecast.GetByCityId(2964574);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Items);
            Assert.IsTrue(result.Items.Count > 0);
            Assert.IsNotNull(result.Items[0]);
        }

        [Test]
        public void GetForecastByCityCoordinatesTest()
        {
            //Does not exist
            var result = Forecast.GetByCoordinates(-1984453.363665, -1984453.363665);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Items);

            //Exist
            result = Forecast.GetByCoordinates(53.363665, -6.255541);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Items);
            Assert.IsTrue(result.Items.Count > 0);
            Assert.IsNotNull(result.Items[0]);
        }


        [Test]
        public void GetDailyByCityNameTest()
        {
            //Does not exist
            var result = Daily.GetByCityName("testcitytest1", "testcitytest1", 14);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Items);

            //Exist
            result = Daily.GetByCityName("Dublin", "Ireland", 14);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Items);
            Assert.IsTrue(result.Items.Count > 0);
            Assert.IsNotNull(result.Items[0]);
        }

        [Test]
        public void GetDailyByCityIdTest()
        {
            //Does not exist
            var result = Daily.GetByCityId(1111111, 5);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Items);

            //Exist
            result = Daily.GetByCityId(2964574, 14);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Items);
            Assert.IsTrue(result.Items.Count > 0);
            Assert.IsNotNull(result.Items[0]);
        }

        [Test]
        public void GetDailyByCityCoordinatesTest()
        {
            //Does not exist
            var result = Daily.GetByCoordinates(-1984453.363665, -1984453.363665, 14);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Items);

            //Exist
            result = Daily.GetByCoordinates(53.363665, -6.255541, 14);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Items);
            Assert.IsTrue(result.Items.Count > 0);
            Assert.IsNotNull(result.Items[0]);
        }
    }
}