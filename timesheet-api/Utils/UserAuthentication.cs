namespace  timesheet_api.Utils
{
    public class UserAuthentication
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool Authenticated { get; set; }
    }   
}