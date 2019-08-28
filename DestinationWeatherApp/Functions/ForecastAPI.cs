using DestinationWeatherApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;

namespace DestinationWeatherApp.Functions
{
    public static class ForecastAPI
    {
        enum WeatherType { Clear, Clouds, Fog, Drizzle, Rain, Thunderstorm, Snow }

        private const string APIKEY = "7570f38dc325ddb220a7877ca8f883b0";
        private static string error = "";


        /// <summary>
        /// Gets the best weather of the forecast that is 5 times of three hours periods.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>

        public static ForecastResult GetBestWeather(string destination, string json, int daysrange)
        {
            var result = JsonConvert.DeserializeObject<WeatherForecast>(json);
            WeatherForecast forecast = result;
            ForecastResult bestweather = new ForecastResult();
            var currentWeather = string.Empty;
            var choosenWeather = string.Empty;
            Dictionary<int, string> _storedWeather = new Dictionary<int, string>();

            // Here we got the selected daysrange we have choosen by the slider in the frontend.
            int timespan = FormatFunction.GetTimespan(daysrange);

            //  Here we get the weather of the forecast list from the json object and store it in a dictionary 
            for (var i = 0; i < timespan; i++)
            {
                if (forecast.list[i].weather[0].Main == WeatherType.Clear.ToString())
                {
                    // Gets the current weather if weather is Clear
                    var key = i;
                    currentWeather = string.Format("{0}", forecast.list[i].weather[0].Main);
                    choosenWeather = currentWeather;
                    GetCurrentBestWeather(key, bestweather, forecast);
                    break;
                }
                else
                {
                    if (forecast.list[i].weather[0].Main == WeatherType.Clouds.ToString())
                    {
                        currentWeather = string.Format("{0}", forecast.list[i].weather[0].Main);
                        _storedWeather.Add(i, currentWeather);
                    }

                    if (forecast.list[i].weather[0].Main == WeatherType.Fog.ToString())
                    {
                        currentWeather = string.Format("{0}", forecast.list[i].weather[0].Main);
                        _storedWeather.Add(i, currentWeather);
                    }

                    if (forecast.list[i].weather[0].Main == WeatherType.Drizzle.ToString())
                    {
                        currentWeather = string.Format("{0}", forecast.list[i].weather[0].Main);
                        _storedWeather.Add(i, currentWeather);
                    }

                    if (forecast.list[i].weather[0].Main == WeatherType.Rain.ToString())
                    {
                        currentWeather = string.Format("{0}", forecast.list[i].weather[0].Main);
                        _storedWeather.Add(i, currentWeather);
                    }

                    if (forecast.list[i].weather[0].Main == WeatherType.Thunderstorm.ToString())
                    {
                        currentWeather = string.Format("{0}", forecast.list[i].weather[0].Main);
                        _storedWeather.Add(i, currentWeather);
                    }

                    if (forecast.list[i].weather[0].Main == WeatherType.Snow.ToString())
                    {
                        currentWeather = string.Format("{0}", forecast.list[i].weather[0].Main);
                        _storedWeather.Add(i, currentWeather);
                    }
                }
            }

            if (choosenWeather != WeatherType.Clear.ToString())
            {
                var firstcloud = _storedWeather.FirstOrDefault(x => x.Value == WeatherType.Clouds.ToString());
                //If best weather is cloads
                if (firstcloud.Value != null)
                {
                    foreach (var item in _storedWeather)
                    {
                        if (item.Value == firstcloud.Value)
                        {
                            choosenWeather = WeatherType.Clouds.ToString();
                            var key = item.Key;
                            GetCurrentBestWeather(key, bestweather, forecast);
                            break;
                        }
                    }
                }

                if (choosenWeather != WeatherType.Clouds.ToString())
                {
                    //If best weather is fog
                    var firstfog = _storedWeather.FirstOrDefault(x => x.Value == WeatherType.Fog.ToString());
                    if (firstfog.Value != null)
                    {
                        foreach (var item in _storedWeather)
                        {
                            if (item.Value == firstfog.Value)
                            {
                                choosenWeather = WeatherType.Fog.ToString();
                                var key = item.Key;
                                GetCurrentBestWeather(key, bestweather, forecast);
                                break;
                            }
                        }
                    }
                }

                if (choosenWeather != WeatherType.Clouds.ToString())
                {
                    if (choosenWeather != WeatherType.Fog.ToString())
                    {
                        //If best weather is drizzle
                        var firstdrizzle = _storedWeather.FirstOrDefault(x => x.Value == WeatherType.Drizzle.ToString());
                        if (firstdrizzle.Value != null)
                        {
                            foreach (var item in _storedWeather)
                            {
                                if (item.Value == firstdrizzle.Value)
                                {
                                    choosenWeather = WeatherType.Drizzle.ToString();
                                    var key = item.Key;
                                    GetCurrentBestWeather(key, bestweather, forecast);
                                    break;
                                }
                            }
                        }
                    }
                }

                if (choosenWeather != WeatherType.Drizzle.ToString())
                {
                    if (choosenWeather != WeatherType.Fog.ToString())
                    {
                        if (choosenWeather != WeatherType.Clouds.ToString())
                        {
                            //If best weather is rain
                            var firstrain = _storedWeather.FirstOrDefault(x => x.Value == WeatherType.Rain.ToString());
                            if (firstrain.Value != null)
                            {
                                foreach (var item in _storedWeather)
                                {
                                    if (item.Value == firstrain.Value)
                                    {
                                        choosenWeather = WeatherType.Rain.ToString();
                                        var key = item.Key;
                                        GetCurrentBestWeather(key, bestweather, forecast);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                if (choosenWeather != WeatherType.Rain.ToString())
                {
                    if (choosenWeather != WeatherType.Drizzle.ToString())
                    {
                        if (choosenWeather != WeatherType.Fog.ToString())
                        {
                            if (choosenWeather != WeatherType.Clouds.ToString())
                            {
                                //If best weather is thunderstorm
                                var firstthunderstorm = _storedWeather.FirstOrDefault(x => x.Value == WeatherType.Thunderstorm.ToString());
                                if (firstthunderstorm.Value != null)
                                {
                                    foreach (var item in _storedWeather)
                                    {
                                        if (item.Value == firstthunderstorm.Value)
                                        {
                                            choosenWeather = WeatherType.Thunderstorm.ToString();
                                            var key = item.Key;
                                            GetCurrentBestWeather(key, bestweather, forecast);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (choosenWeather != WeatherType.Thunderstorm.ToString())
                {
                    if (choosenWeather != WeatherType.Rain.ToString())
                    {
                        if (choosenWeather != WeatherType.Drizzle.ToString())
                        {
                            if (choosenWeather != WeatherType.Fog.ToString())
                            {
                                if (choosenWeather != WeatherType.Clouds.ToString())
                                {
                                    //If best weather is snow
                                    var firstsnow = _storedWeather.FirstOrDefault(x => x.Value == WeatherType.Snow.ToString());
                                    foreach (var item in _storedWeather)
                                    {
                                        if (item.Value == firstsnow.Value)
                                        {
                                            choosenWeather = WeatherType.Snow.ToString();
                                            var key = item.Key;
                                            GetCurrentBestWeather(key, bestweather, forecast);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return bestweather;
        }
        /// <summary>
        /// Gets the worst weather of the forecast that is 8 times of three hours periods.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>

        public static ForecastResult GetWorstWeather(string destination, string json, int daysrange)
        {
            var result = JsonConvert.DeserializeObject<WeatherForecast>(json);
            WeatherForecast forecast = result;
            ForecastResult worstweather = new ForecastResult();
            var currentWeather = string.Empty;
            var currentDescription = string.Empty;
            var choosenWeather = string.Empty;
            Dictionary<int, string> _storedWeather = new Dictionary<int, string>();

            // Here we got the selected daysrange we have choosen by the slider in the frontend.
            int timespan = FormatFunction.GetTimespan(daysrange);

            //  Here we get the weather of the forecast list from the json object and store it in a dictionary                                                                    */
            if (forecast != null)
            {
                for (var i = 0; i < timespan; i++)
                {
                    if (forecast.list[i].weather[0].Main == WeatherType.Snow.ToString())
                    {
                        var key = i;
                        currentWeather = string.Format("{0}", forecast.list[i].weather[0].Main);
                        choosenWeather = currentWeather;
                        GetCurrentWorstWeather(key, worstweather, forecast);
                    }
                    else
                    {
                        if (forecast.list[i].weather[0].Main == WeatherType.Clear.ToString())
                        {
                            currentWeather = string.Format("{0}", forecast.list[i].weather[0].Main);
                            _storedWeather.Add(i, currentWeather);
                        }

                        if (forecast.list[i].weather[0].Main == WeatherType.Clouds.ToString())
                        {
                            currentWeather = string.Format("{0}", forecast.list[i].weather[0].Main);
                            _storedWeather.Add(i, currentWeather);
                        }

                        if (forecast.list[i].weather[0].Main == WeatherType.Fog.ToString())
                        {
                            currentWeather = string.Format("{0}", forecast.list[i].weather[0].Main);
                            _storedWeather.Add(i, currentWeather);
                        }

                        if (forecast.list[i].weather[0].Main == WeatherType.Drizzle.ToString())
                        {
                            currentWeather = string.Format("{0}", forecast.list[i].weather[0].Main);
                            _storedWeather.Add(i, currentWeather);
                        }

                        if (forecast.list[i].weather[0].Main == WeatherType.Rain.ToString())
                        {
                            currentWeather = string.Format("{0}", forecast.list[i].weather[0].Main);
                            _storedWeather.Add(i, currentWeather);
                        }

                        if (forecast.list[i].weather[0].Main == WeatherType.Thunderstorm.ToString())
                        {
                            currentWeather = string.Format("{0}", forecast.list[i].weather[0].Main);
                            _storedWeather.Add(i, currentWeather);
                        }
                    }
                }

                // If thunderstorm is the worst weather in the forecast
                if (choosenWeather != WeatherType.Snow.ToString())
                {
                    // Here we got the first thunderstorm in the forecast
                    var firstthunderstorm = _storedWeather.FirstOrDefault(x => x.Value == WeatherType.Thunderstorm.ToString());
                    if (firstthunderstorm.Value != null)
                    {
                        foreach (var item in _storedWeather)
                        {
                            // Here we will got the matching item to the first thunderstorm object in the forecast
                            if (item.Value == firstthunderstorm.Value)
                            {
                                // Now we set a value of choosenWeather
                                choosenWeather = WeatherType.Thunderstorm.ToString();
                                var key = item.Key;
                                // Here we sets the values to the returning object worstweather
                                GetCurrentWorstWeather(key, worstweather, forecast);
                                break;
                            }
                        }
                    }
                }

                // If rain is the worst weather in the forecast
                if (choosenWeather != WeatherType.Snow.ToString())
                {
                    if (choosenWeather != WeatherType.Thunderstorm.ToString())
                    {
                        var firstrain = _storedWeather.FirstOrDefault(x => x.Value == WeatherType.Rain.ToString());

                        if (firstrain.Value != null)
                        {
                            foreach (var item in _storedWeather)
                            {
                                if (item.Value == firstrain.Value)
                                {
                                    choosenWeather = WeatherType.Rain.ToString();
                                    var key = item.Key;
                                    GetCurrentWorstWeather(key, worstweather, forecast);
                                    break;
                                }
                            }
                        }
                    }
                }


                if (choosenWeather != WeatherType.Snow.ToString())
                {
                    if (choosenWeather != WeatherType.Thunderstorm.ToString())
                    {
                        if (choosenWeather != WeatherType.Rain.ToString())
                        {
                            // If Drizzle is the worst weather in the forecast
                            var firstDrizzle = _storedWeather.FirstOrDefault(x => x.Value == WeatherType.Drizzle.ToString());

                            if (firstDrizzle.Value != null)
                            {
                                foreach (var item in _storedWeather)
                                {
                                    if (item.Value == firstDrizzle.Value)
                                    {
                                        choosenWeather = WeatherType.Drizzle.ToString();
                                        var key = item.Key;
                                        GetCurrentWorstWeather(key, worstweather, forecast);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (choosenWeather != WeatherType.Snow.ToString())
                {
                    if (choosenWeather != WeatherType.Thunderstorm.ToString())
                    {
                        if (choosenWeather != WeatherType.Rain.ToString())
                        {
                            if (choosenWeather != WeatherType.Drizzle.ToString())
                            {
                                // If fog is the worst weather at the forecast
                                var firstFog = _storedWeather.FirstOrDefault(x => x.Value == WeatherType.Fog.ToString());

                                if (firstFog.Value != null)
                                {
                                    foreach (var item in _storedWeather)
                                    {
                                        if (item.Value == firstFog.Value)
                                        {
                                            choosenWeather = WeatherType.Fog.ToString();
                                            var key = item.Key;
                                            GetCurrentWorstWeather(key, worstweather, forecast);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (choosenWeather != WeatherType.Snow.ToString())
                {
                    if (choosenWeather != WeatherType.Thunderstorm.ToString())
                    {
                        if (choosenWeather != WeatherType.Rain.ToString())
                        {
                            if (choosenWeather != WeatherType.Drizzle.ToString())
                            {
                                if (choosenWeather != WeatherType.Fog.ToString())
                                {
                                    // If it is clouds as the worst weather at the forecast
                                    var firstcloud = _storedWeather.FirstOrDefault(x => x.Value == WeatherType.Clouds.ToString());

                                    foreach (var item in _storedWeather)
                                    {
                                        if (item.Value == firstcloud.Value)
                                        {
                                            choosenWeather = WeatherType.Clouds.ToString();
                                            var key = item.Key;
                                            GetCurrentWorstWeather(key, worstweather, forecast);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return worstweather;
        }

        /// <summary>
        /// Gets the properties to the current weather from the WeatherForecast class and sets the values to the properties of the ForecastResult class that both bestweather and worstweather uses.                                                                                                                                                                                                                                                                                                                                                                                                   
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bestweather"></param>
        /// <param name="forecast"></param>
        /// <returns></returns>
        private static ForecastResult GetCurrentBestWeather(int key, ForecastResult bestweather, WeatherForecast forecast)
        {
            bestweather.Dt = string.Format("{0}", FormatFunction.getDate(forecast.list[key].Dt));
            bestweather.City = string.Format("{0}", forecast.city.Name);
            bestweather.Temperature = string.Format("{0}", FormatFunction.getCelsius(forecast.list[key].main.Temp));
            bestweather.Weather = string.Format("{0}", forecast.list[key].weather[0].Main);
            bestweather.Description = string.Format("{0}", forecast.list[key].weather[0].Description);

            return bestweather;
        }

        /// <summary>
        /// Gets the properties to the current weather from the WeatherForecast class and sets the values to the properties of the ForecastResult class that both bestweather and worstweather uses.                                                                                                                                                                                                                                                                                                                                                                                                   
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bestweather"></param>
        /// <param name="forecast"></param>
        /// <returns></returns>
        private static ForecastResult GetCurrentWorstWeather(int key, ForecastResult worstweather, WeatherForecast forecast)
        {
            worstweather.Dt = string.Format("{0}", FormatFunction.getDate(forecast.list[key].Dt));
            worstweather.City = string.Format("{0}", forecast.city.Name);
            worstweather.Temperature = string.Format("{0}", FormatFunction.getCelsius(forecast.list[key].main.Temp));
            worstweather.Weather = string.Format("{0}", forecast.list[key].weather[0].Main);
            worstweather.Description = string.Format("{0}", forecast.list[key].weather[0].Description);

            return worstweather;
        }
    }
}