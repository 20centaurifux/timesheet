using System;
using System.Text.RegularExpressions;

namespace timesheet_api.Utils
{
    public static class Converter
    {
        public static int TimeStringToMinutes(string time)
        {
            var regex = new Regex("(\\d{1,2}):(\\d{1,2})");
            var m = regex.Match(time);
            int minutes = 0;

            if(m.Groups.Count == 3)
            {
                minutes = Convert.ToInt32(m.Groups[1].ToString()) * 60;
                minutes += Convert.ToInt32(m.Groups[2].ToString());
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