namespace  timesheet_api.Authentication
{
    public class UserAuthentication
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool Authenticated { get; set; }
    }   
}