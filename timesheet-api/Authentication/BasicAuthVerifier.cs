using Bazinga.AspNetCore.Authentication.Basic;
using System.Linq;
using timesheet_api.Models;
using timesheet_api.Data;
using timesheet_api.Utils;

namespace timesheet_api.Authentication
{
    public class BasicAuthVerifier : IBasicCredentialVerifier
    {
        private readonly TimesheetContext _context;
        private readonly UserAuthentication _authentication;
        
        public BasicAuthVerifier(TimesheetContext context, UserAuthentication authentication)
        {
            _context = context;
            _authentication = authentication;
        }

        public System.Threading.Tasks.Task<bool> Authenticate(string username, string password)
        {
            var success = false;

            try
            {
                _authentication.UserName = username.ToLower();
                _authentication.PasswordHash = Utils.Crypto.PasswordHash(password);
                _authentication.Authenticated = false;

                var user = _context.Users.Single(u => u.Name.ToLower().Equals(_authentication.UserName));

                if(user != null)
                {
                    success = _authentication.PasswordHash.Equals(user.Password);

                    if(success)
                    {
                        _authentication.Authenticated = true;
                    }
                }
            }
            catch (System.InvalidOperationException) {}

            return System.Threading.Tasks.Task.FromResult(success);
        }
    }
}