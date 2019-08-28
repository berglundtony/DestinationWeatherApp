using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DestinationWeatherApp.Models
{
    public class ForecastResult
    {
        public string Dt { get; set; }
        public string City { get; set; }
        public string Temperature { get; set; }
        public string Weather { get; set; }
        public string Description { get; set; }
    }
}