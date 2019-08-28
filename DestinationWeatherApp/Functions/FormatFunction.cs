using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DestinationWeatherApp.Functions
{
    public static class FormatFunction
    {
        /// <summary>
        /// Convert milliseconds to datetime. The time is wintertime in the API so we checks if the time is the same as datetime.Now otherwise it adds one hour.
        /// </summary>
        /// <param name="millisecond"></param>
        /// <returns></returns>
        public static DateTime getDate(double millisecond)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(millisecond).ToLocalTime();
            if (day.Hour == DateTime.Now.Hour)
            {
                return day.ToLocalTime();
            }
            else
            {
                return day.AddHours(1);
            }
        }

        /// <summary>
        /// Gets the celsius degrees from Kelvin degrees.
        /// </summary>
        /// <param name="kelvin"></param>
        /// <returns></returns>
        /// 
        const double KelvinToCelciusOffset = -273.15;
        public static double getCelsius(double kelvin)
        {
            double celsius = kelvin + KelvinToCelciusOffset;
            return Math.Round(celsius);
        }

        public static int GetTimespan(int daysrange) {
            int timespan = 0;
            switch (daysrange)
            {
                case 1:
                    timespan = 8;
                        break;
                case 2:
                    timespan = 16;
                        break;
                case 3:
                    timespan = 24;
                    break;
                case 4:
                    timespan = 32;
                    break;
                case 5:
                    timespan = 40;
                    break;
                case 6:
                    timespan = 48;
                    break;
                case 7:
                    timespan = 48;
                    break;

            }
            return timespan;
        }
    }
}