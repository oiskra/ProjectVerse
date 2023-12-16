using Microsoft.AspNetCore.Authorization;
using projectverseAPI.Constants;

namespace projectverseAPI.Handlers
{
    public class UpdateUserRequirement : IAuthorizationRequirement
    { }

    public class UpdateUserAuthorizationHandler : AuthorizationHandler<UpdateUserRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserAuthorizationHandler(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UpdateUserRequirement requirement)
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
