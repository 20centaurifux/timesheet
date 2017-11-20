using System;

namespace timesheet_api.Models
{
    public class Entry
    {
        public long Id { get; set; }
        public User User { get; set; }
        public int Minutes { get; set; }
        public Task Task { get; set; }
        public Project Project { get; set; }
        public DateTime Date { get; set; }
    }
}