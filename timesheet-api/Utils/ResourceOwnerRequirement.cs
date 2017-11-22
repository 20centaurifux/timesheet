using  Microsoft.AspNetCore.Authorization;

namespace timesheet_api.Utils
{
    public class ResourceOwnerRequirement : IAuthorizationRequirement
    {
        public ResourceOwnerRequirement(int pathOffset)
        {
            PathOffset = pathOffset;
        }

        public int PathOffset { get; set; }
    }   
}