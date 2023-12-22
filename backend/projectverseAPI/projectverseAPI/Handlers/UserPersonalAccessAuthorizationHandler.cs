using Microsoft.AspNetCore.Authorization;
using projectverseAPI.Constants;

namespace projectverseAPI.Handlers
{
    public class UserPersonalAccessRequirement : IAuthorizationRequirement
    { }

    public class UserPersonalAccessAuthorizationHandler : AuthorizationHandler<UserPersonalAccessRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserPersonalAccessAuthorizationHandler(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserPersonalAccessRequirement requirement)
        {
            RouteValueDictionary? routeValues = _httpContextAccessor.HttpContext?.Request.RouteValues;

            string? userId = context
                .User
                .Claims
                .FirstOrDefault(c => c.Type == ClaimNameConstants.Identifier)?.Value;

            
            if (userId is null || routeValues is null || !routeValues.ContainsKey("userId"))
                return Task.CompletedTask;

            if ((string)routeValues["userId"]! == userId)
                context.Succeed(requirement);
            
            return Task.CompletedTask;
        }
    }
}
