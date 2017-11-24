using System;
using System.Text.RegularExpressions;

namespace timesheet_api.Utils
{
    public static class Converter
    {
        public static int TimeStringToMinutes(string time)
        {
            var regex = new Regex("(\\d{1,2}):(\\d{1,2})");
            var match = regex.Match(time);
            int minutes = 0;

            if(match.Groups.Count == 3)
            {
                var hours = Convert.ToInt32(match.Groups[1].ToString());

                minutes = Convert.ToInt32(match.Groups[2].ToString());

                if(hours < 0 || minutes < 0)
                {
                    throw new ArgumentException();
                }

                if(hours > 23 || minutes > 59)
                {
                    throw new ArgumentException();
                }

                minutes += hours * 60;
            }
            else
            {
                throw new ArgumentException();
            }

            return minutes;
        }

        public static string MinutesToTimeString(int minutes)
        {
            int hours = minutes / 60;
            
            minutes -= hours * 60;

            return string.Format("{0:00}:{1:00}", hours, minutes);
        }
    }
}