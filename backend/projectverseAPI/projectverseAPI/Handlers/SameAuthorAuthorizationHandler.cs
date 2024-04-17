﻿using Microsoft.AspNetCore.Authorization;
using projectverseAPI.Constants;
using projectverseAPI.Interfaces.Marker;

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
                .FirstOrDefault(c => c.Type == ClaimNameConstants.Identifier)?.Value;

            if (!Guid.TryParse(userId, out Guid parsedUserId))
                return Task.CompletedTask;

            if (resource.AuthorId == parsedUserId)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }


}
