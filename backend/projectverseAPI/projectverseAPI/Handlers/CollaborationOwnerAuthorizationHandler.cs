using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace projectverseAPI.Handlers
{
    public class CollaborationOwnerRequirement : IAuthorizationRequirement
    { }

    public class CollaborationOwnerAuthorizationHandler : AuthorizationHandler<CollaborationOwnerRequirement>
    {

        private readonly ProjectVerseContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;

        public CollaborationOwnerAuthorizationHandler(ProjectVerseContext context, IHttpContextAccessor contextAccessor)
        {
            _dbContext = context;
            _contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CollaborationOwnerRequirement requirement)
        {
            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return Task.CompletedTask;

            bool match;

            //id usera na requescie
            var userIdParsed = Guid.TryParse(
                context.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value, 
                out Guid userId);

            //id collaboraacji
            var collaborationIdParsed = Guid.TryParse(
                (string?)_contextAccessor.HttpContext.Request.RouteValues["collaborationId"],
                out Guid collaborationId);
            
            if(!collaborationIdParsed)
            {
                var applicantIdParsed = Guid.TryParse(
                    (string?)_contextAccessor.HttpContext.Request.RouteValues["applicantId"],
                    out Guid applicantId);
                
                if(!applicantIdParsed)
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                match = _dbContext.CollaborationApplicants
                    .Include(ca => ca.AppliedCollaboration)
                    .Where(ca => ca.Id == applicantId)
                    .Select(ca => ca.AppliedCollaboration)                   
                    .Any(c => c.AuthorId == userId);
            }
            else
                match = _dbContext.Collaborations.Any(c => c.AuthorId == userId && c.Id == collaborationId);

            if(!match)
            {
                context.Fail();
                return Task.CompletedTask;
            }
                
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
