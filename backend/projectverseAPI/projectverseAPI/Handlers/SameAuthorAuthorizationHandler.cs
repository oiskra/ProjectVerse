using Microsoft.AspNetCore.Authorization;
using projectverseAPI.Interfaces;

namespace projectverseAPI.Handlers
{
    public class SameAuthorRequirement : IAuthorizationRequirement
    { }

    public class SameAuthorAuthorizationHandler : AuthorizationHandler<SameAuthorRequirement, IAuthorizableByAuthor>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameAuthorRequirement requirement, IAuthorizableByAuthor resource)
        {
            string? userId = context
                .User
                .Claims
                .FirstOrDefault(c => c.Type == "id")?.Value;

            if (!Guid.TryParse(userId, out Guid parsedUserId))
                return Task.CompletedTask;

            if (resource.AuthorId == parsedUserId)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }


}
