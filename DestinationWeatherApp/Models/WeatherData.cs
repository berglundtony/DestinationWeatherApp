using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Xml;
using DestinationWeatherApp.Functions;
using System.ComponentModel.DataAnnotations;

namespace DestinationWeatherApp.Models
{
    public class WeatherData
    {
        public string CurrentTime { get; set; }
        public string Destination { get; set; }
        public string Weather { get;set; }
        public string Description { get; set; }
        public string Cloudyness { get; set; }
        public float? Temp { get; set; }
    }
}
