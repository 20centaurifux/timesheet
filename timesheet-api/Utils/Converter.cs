using System;
using System.Text.RegularExpressions;

namespace timesheet_api.Utils
{
    public static class Converter
    {
        public static int TimeStringToSeconds(string time)
        {
            var regex = new Regex("(\\d{1,2}):(\\d{1,2}):(\\d{1,2})");
            var match = regex.Match(time);
            int seconds = 0;

            if(match.Groups.Count == 4)
            {
                var hours = Convert.ToInt32(match.Groups[1].ToString());
                var minutes = Convert.ToInt32(match.Groups[2].ToString());

                seconds = Convert.ToInt32(match.Groups[3].ToString());

                if(hours < 0 || minutes < 0 || seconds < 0)
                {
                    throw new ArgumentException();
                }

                if(hours > 23 || minutes > 59 || seconds > 60)
                {
                    throw new ArgumentException();
                }

                minutes += hours * 60;
                seconds += minutes * 60;
            }
            else
            {
                throw new ArgumentException();
            }

            return seconds;
        }

        public static string SecondsToTimeString(int seconds)
        {
            int minutes = (int)Math.Floor((double)seconds / 60);
            int hours = (int)Math.Floor((double)minutes / 60);

            minutes -= hours * 60;
            seconds -= hours * 3600 + minutes * 60;

            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
    }
}