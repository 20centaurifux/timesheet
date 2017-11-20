namespace timesheet_api.Models
{
    public class Project
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ProjectGroup Group { get; set; }
    }
}