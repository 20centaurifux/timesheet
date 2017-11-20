namespace timesheet_api.Models
{
    public class Task
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Productive { get; set; }
        public ProjectGroup ProjectGroup { get; set; }
    }
}