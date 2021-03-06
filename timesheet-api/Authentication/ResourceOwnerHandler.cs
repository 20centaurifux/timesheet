using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace timesheet_api.Authentication
{
    public class ResourceOwnerHandler : AuthorizationHandler<PartOfPathRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserAuthentication _authentication;

        public ResourceOwnerHandler(IHttpContextAccessor contextAccessor, UserAuthentication authentication)
        {
            _contextAccessor = contextAccessor;
            _authentication = authentication;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PartOfPathRequirement requirement)
        {
            var httpContext = _contextAccessor.HttpContext;
            var parts = httpContext.Request.Path.ToString().Split("/");

            if(parts[requirement.PathOffset].Equals(_authentication.UserName, StringComparison.CurrentCultureIgnoreCase))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}