using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using BogdanM.LocationServices.GoogleMaps;
using DestinationWeatherApp.Functions;
using DestinationWeatherApp.Models;
using Newtonsoft.Json;

namespace DestinationWeatherApp.Controllers
{
    public class HomeController : Controller
    {
        string error = "";
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// The method returns the coordinates the adress and the city we got from the  daparture destinition and the arriving destination.
        /// </summary>
        /// <param name="fromaddress"></param>
        /// <param name="toaddress"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCoordinates(string fromaddress, string toaddress)
        {
            List<Geolocation> _geo = new List<Geolocation>();

            if (fromaddress != "" && toaddress != "")
            {
                _geo.Add(MapFunctions.Locate(fromaddress));
                _geo.Add(MapFunctions.Locate(toaddress));
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(_geo);

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Here we get the weather data at the current time we search for the rout between the two destinations.
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetWeather(string destination)
        {
            Geolocation geolocation = MapFunctions.Locate(destination);
            WeatherData todayWeather = new WeatherData();
            WeatherAPI weather = new WeatherAPI(geolocation.Latitude, geolocation.Longitude);
            todayWeather = weather.GetWeather(destination);
            return Json(todayWeather, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// We return the best weather from the forecast at the daysrange we have choosen by the slider in the frontend.
        /// </summary>

        private string APIKEY = "7570f38dc325ddb220a7877ca8f883b0";
        public JsonResult GetWeatherForecast(string Destination, string DaysRange)
        {
            // Here we get latitude and longitude from the destination
            Geolocation geolocation = MapFunctions.Locate(Destination);
            string json = "";
            int daysrange = int.Parse(DaysRange);
            // We use WebClient to get the information from the Open WeatheMap API
            using (WebClient web = new WebClient())
            {
                var query = "http://api.openweathermap.org/data/2.5/forecast?lat=" + geolocation.Latitude + "&lon=" + geolocation.Longitude + "&appid=" + APIKEY;
                json = web.DownloadString(query);
            }

            return Json(ForecastAPI.GetBestWeather(Destination, json, daysrange), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Uses WebClient to get the right URL to Open Weather Map and uses the Locate method to get the latitude and longitude from Google
        /// maps API, if the destination can't be found in Open Weather Maps API we uses the coordinate from googlemaps API and uses them in Open Weather Maps API to get the worst posible weather for a timeframe.
        /// we have choosen by the daysrange slider in the frontend, at least 8 times. We return the first worst weather we find in the selected timeframe.
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public JsonResult GetWorstWeatherForecast(string Destination, string DaysRange)
        {
            Geolocation geolocation = new Geolocation();
            geolocation = MapFunctions.Locate(Destination);
            string json = "";
            int daysrange = int.Parse(DaysRange);
       
            using (WebClient web = new WebClient())
            {
                try
                {
                    var query = "http://api.openweathermap.org/data/2.5/forecast?lat=" + geolocation.Latitude + "&lon=" + geolocation.Longitude + "&appid=" + APIKEY;
                    json = web.DownloadString(query);
                }
                catch(Exception ex)
                {
                    error = ex.Message;
                } 
            }

            return Json(ForecastAPI.GetWorstWeather(Destination, json, daysrange), JsonRequestBehavior.AllowGet);
        }
    }
}