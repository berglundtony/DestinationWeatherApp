using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DestinationWeatherApp.Models
{
    public class WeatherForecast
    {
        public city city { get; set; }
        public List<list> list { get; set; }
    }

    public class weather
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class city
    {
        public string Name { get; set; }
    }

    public class main
    {
        public double Temp { get; set; }
    }

    public class list
    {
        public double Dt { get; set; }
        public main main { get; set; }
        public List<weather> weather { get; set; }
    }
}
