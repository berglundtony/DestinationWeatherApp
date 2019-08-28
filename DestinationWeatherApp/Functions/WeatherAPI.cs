using DestinationWeatherApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;

namespace DestinationWeatherApp.Functions
{
    public class WeatherAPI
    {
        private const string APIKEY = "7570f38dc325ddb220a7877ca8f883b0";
        private static string CurrentURL;
        private string currentWeather = "";
        public RootObject weather_object;
        private string error = "";

        /// <summary>
        /// This is the constructor to the class ForecastAPI with two inparameters. Here we got two
        /// methods SetCurrentURLCoordinates() and GetJson() that always activates when we uses the ForecastAPI class
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>

        public WeatherAPI(double latitude, double longitude)
        {
            SetCurrentURLCoordinates(latitude, longitude);
            GetJson();
        }

        /// <summary>
        /// Use the parameters lat and lon to get the current URL to Open Weather Map API.
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>

        private static void SetCurrentURLCoordinates(double lat, double lon)
        {
            CurrentURL = "http://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&APPID=" + APIKEY;
        }

        /// <summary>         
        /// Get json string from Open Weather Map API deserialize it to a class called RootObject and store the values in the private variable weather_object 
        /// </summary>
        /// <returns>weather_object variable as RootObject</returns>

        public RootObject GetJson()
        {
            string json = "";
            using (WebClient web = new WebClient())
            {
                try
                {
                    json = web.DownloadString(CurrentURL);
                    var result = JsonConvert.DeserializeObject<RootObject>(json);
                    weather_object = result;

                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
                return weather_object;
            }
        }
        /// <summary>
        /// We uses the weather_object we got from the GetJson method calling from WeatherAPI:s constructor and set values to the WeatheData model the method returns.
        /// </summary>
        /// <param name="destination"></param>
        /// <returns> WeatherData</returns>
        public WeatherData GetWeather(string destination)
        {
            string temp_string = "";
            WeatherData data = new WeatherData();
            data.Weather = weather_object != null ? weather_object.weather[0].main : null;
            data.Description = weather_object != null ? weather_object.weather[0].description : null;
            data.CurrentTime = weather_object != null ? string.Format("{0}", FormatFunction.getDate(weather_object.dt)) : null;
            temp_string = weather_object != null ? string.Format("{0}", FormatFunction.getCelsius(weather_object.main.temp).ToString()) : null;
            data.Temp = float.Parse(string.Format("{0}",temp_string, CultureInfo.InvariantCulture));
            data.Cloudyness = currentWeather = weather_object != null ? weather_object.clouds.all.ToString() : null;
            data.Destination = destination;
            return data;
        }
    }
}