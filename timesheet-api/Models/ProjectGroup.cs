using System.Collections.Generic;

namespace timesheet_api.Models
{
    public class ProjectGroup
    {
        public long Id { get; set; }
        public string Name { get; set; } 
        public ICollection<Project> Projects { get; set; }
    }
}