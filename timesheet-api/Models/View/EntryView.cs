using timesheet_api.Models;
using timesheet_api.Utils;

namespace timesheet_api.Models.View
{
    public class EntryView
    {
        public EntryView() {}

        public static EntryView FromEntry(Entry entry)
        {
            var e = new EntryView();

            e.id = entry.Id;
            e.hours = Converter.SecondsToTimeString(entry.Seconds);
            e.task = entry.Task.Name;

            if(entry.Project != null)
            {
                e.project = entry.Project.Name;
            }

            return e;
        }

        public long id { get; set; }
        public string hours { get; set; }
        public string task { get; set; }
        public string project { get; set; }
    }
}