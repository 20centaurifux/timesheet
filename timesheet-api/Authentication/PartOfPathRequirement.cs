using  Microsoft.AspNetCore.Authorization;

namespace timesheet_api.Authentication
{
    public class PartOfPathRequirement : IAuthorizationRequirement
    {
        public PartOfPathRequirement(int pathOffset)
        {
            PathOffset = pathOffset;
        }

        public int PathOffset { get; set; }
    }   
}