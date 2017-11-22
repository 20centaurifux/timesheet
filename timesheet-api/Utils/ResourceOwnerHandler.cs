using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace timesheet_api.Utils
{
    public class ResourceOwnerHandler : AuthorizationHandler<ResourceOwnerRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserAuthentication _authentication;

        public ResourceOwnerHandler(IHttpContextAccessor contextAccessor, UserAuthentication authentication)
        {
            _contextAccessor = contextAccessor;
            _authentication = authentication;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOwnerRequirement requirement)
        {
            var httpContext = _contextAccessor.HttpContext;
            var parts = httpContext.Request.Path.ToString().Split("/");

            if(parts[requirement.PathOffset].Equals(_authentication.UserName))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}