using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Services
{
    public class CollaborationApplicantsService : ICollaborationApplicantsService
    {
        private readonly ProjectVerseContext _context;
        private readonly IAuthenticationService _authenticationService;

        public CollaborationApplicantsService(
            ProjectVerseContext context,
            IAuthenticationService authenticationService)
        {
            _context = context;
            _authenticationService = authenticationService;
        }

        public async Task ChangeApplicationStatus(Guid applicantId, ChangeApplicationStatusRequestDTO applicationStateRequestDTO)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var applicant = await _context.CollaborationApplicants
                .FirstOrDefaultAsync(a => a.Id == applicantId);

                if (applicant is null)
                    throw new ArgumentException("Applicant doesn't exist");

                if (applicant.ApplicationStatus == applicationStateRequestDTO.ApplicationStatus)
                    return;

                await HandleStateChange(
                    applicant.AppliedCollaborationId,
                    applicant.ApplicationStatus,
                    (ApplicationStatus)applicationStateRequestDTO.ApplicationStatus!);

                applicant.ApplicationStatus = (ApplicationStatus)applicationStateRequestDTO.ApplicationStatus!;
                _context.CollaborationApplicants.Update(applicant);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (ArgumentException argE)
            {
                await transaction.RollbackAsync();
                throw new ArgumentException(argE.Message);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }

        }

        public async Task<Guid> ApplyForCollaboration(Guid collaborationId, Guid collaborationPositionId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var collaboration = await _context.Collaborations
               .Include(c => c.CollaborationPositions)
               .Include(c => c.CollaborationApplicants)
               .FirstOrDefaultAsync(c => c.Id == collaborationId);

                if (collaboration is null)
                    throw new ArgumentException("Collaboration doesn't exist.");

                var position = collaboration.CollaborationPositions
                    .FirstOrDefault(c => c.Id == collaborationPositionId);

                if (position is null)
                    throw new ArgumentException("Collaboration position doesn't exist.");

                var currUser = await _authenticationService.GetCurrentUser();

                if (currUser is null)
                    throw new Exception("Cannot get current user.");

                if (collaboration.CollaborationApplicants.Any(ca =>
                    ca.AppliedPositionId == collaborationPositionId && ca.ApplicantUserId == Guid.Parse(currUser.Id)))
                    throw new InvalidOperationException("User already applied for this position.");

                var newApplicant = new CollaborationApplicant
                {
                    Id = Guid.NewGuid(),
                    ApplicantUserId = new Guid(currUser.Id),
                    ApplicantUser = currUser,
                    AppliedPositionId = position.Id,
                    AppliedPosition = position,
                    AppliedCollaborationId = collaboration.Id,
                    AppliedCollaboration = collaboration,
                    ApplicationStatus = ApplicationStatus.Pending,
                    AppliedOn = DateTime.Now
                };

                await _context.CollaborationApplicants.AddAsync(newApplicant);

                if (collaboration.CollaborationApplicants is null)
                    collaboration.CollaborationApplicants = new List<CollaborationApplicant>() { newApplicant };
                else
                    collaboration.CollaborationApplicants.Add(newApplicant);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return newApplicant.Id;
            }
            catch (ArgumentException argE)
            {
                await transaction.RollbackAsync();
                throw new ArgumentException(argE.Message);
            }
            catch (InvalidOperationException ioE)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException(ioE.Message);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

/*        public async Task<List<CollaborationApplicant>> GetCollaborationApplicants(Guid collaborationId)
        {
            //redo pod CollaborationApplicants?
            var collaboration = await _context.Collaborations
                .Include(c => c.CollaborationApplicants)
                .ThenInclude(c => c.ApplicantUser)
                .FirstOrDefaultAsync(c => c.Id == collaborationId);

            if (collaboration is not null)
                return collaboration!.CollaborationApplicants!.ToList();

            return new List<CollaborationApplicant>();
        }*/

        public async Task RemoveApplicantForCollaboration(Guid applicantId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var applicantToDelete = await _context.CollaborationApplicants
                .FirstOrDefaultAsync(a => a.Id == applicantId);

                if (applicantToDelete is null)
                    throw new ArgumentException("Applicant doesn't exist");

                if (applicantToDelete.ApplicationStatus is ApplicationStatus.Accepted)
                {
                    var collaboration = await _context.Collaborations
                        .Include(c => c.CollaborationApplicants)
                        .FirstOrDefaultAsync(c => c.Id == applicantToDelete.AppliedCollaborationId);

                    if (
                        collaboration is not null &&
                        collaboration.CollaborationApplicants is not null)
                    {
                        collaboration.PeopleInvolved -= 1;
                        collaboration.CollaborationApplicants.Remove(applicantToDelete);
                        _context.Collaborations.Update(collaboration);
                    }
                }

                _context.CollaborationApplicants.Remove(applicantToDelete);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (ArgumentException argE)
            {
                await transaction.RollbackAsync();
                throw new ArgumentException(argE.Message);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

        public async Task HandleStateChange(
            Guid collaborationId,
            ApplicationStatus oldStatus,
            ApplicationStatus newStatus)
        {
            var collaboration = await _context.Collaborations
               .FirstOrDefaultAsync(c => c.Id == collaborationId);

            if (collaboration is null)
                throw new ArgumentException("Collaboration doesn't exist");

            if (oldStatus == ApplicationStatus.Accepted)
                collaboration.PeopleInvolved -= 1;
            else if (newStatus == ApplicationStatus.Accepted)
                collaboration.PeopleInvolved += 1;
            else return;

            _context.Collaborations.Update(collaboration);
        }
    }
}
