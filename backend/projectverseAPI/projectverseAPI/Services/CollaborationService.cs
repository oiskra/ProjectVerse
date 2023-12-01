using AutoMapper;
using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Services
{
    public class CollaborationService : ICollaborationService
    {
        private readonly ProjectVerseContext _context;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public CollaborationService(
            ProjectVerseContext context,
            IMapper mapper,
            IAuthenticationService authenticationService)
        {
            _context = context;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        public async Task<Guid> CreateCollaboration(CreateCollaborationRequestDTO collaborationDTO)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var collaboration = _mapper.Map<Collaboration>(collaborationDTO);

                var currentUser = await _authenticationService.GetCurrentUser();

                if (currentUser is null)
                    throw new Exception("Cannot get current user.");
                
                collaboration.Author = currentUser;
                collaboration.AuthorId = Guid.Parse(currentUser.Id);

                var addedCollaboration = await _context.Collaborations.AddAsync(collaboration);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return addedCollaboration.Entity.Id;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteCollaborationById(Guid collaborationId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var collaborationToDelete = await _context.Collaborations
                    .Where(c => c.Id == collaborationId)
                    .Include(c => c.Technologies)
                    .Include(c => c.CollaborationPositions)
                    .FirstOrDefaultAsync();

                if (collaborationToDelete is null)
                    throw new ArgumentException("Collaboration doesn't exist");

                _context.Collaborations.Remove(collaborationToDelete);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
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

        public async Task<List<Collaboration>> GetAllCollaborations()
        {
            var collaborations = await _context.Collaborations
                .Include(c => c.Author)
                .Include(c => c.CollaborationPositions)
                .Include(c => c.Technologies)
                .ToListAsync();
            return collaborations;
        }

        public async Task<List<Collaboration>> GetAllCollaborationsByUserId(Guid userId)
        {
            var collaborations = await _context.Collaborations
                .Where(c => c.AuthorId == userId)
                .Include(c => c.Author)
                .Include(c => c.CollaborationPositions)
                .Include(c => c.Technologies)
                .ToListAsync();
            return collaborations;
        }

        public async Task<Collaboration?> GetCollaborationById(Guid collaborationId)
        {
            var collaboration = await _context.Collaborations
                .Where(c => c.Id.Equals(collaborationId))
                .Include(c => c.Technologies)
                .Include(c => c.CollaborationPositions)
                .Include(c => c.CollaborationApplicants!)
                    .ThenInclude(ca => ca.ApplicantUser)
                .Include(c => c.Author)
                .FirstOrDefaultAsync();
            
            return collaboration;
        }

        public async Task UpdateCollaboration(UpdateCollaborationRequestDTO collaborationDTO)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var exists = _context.Collaborations.Any(c => c.Id == collaborationDTO.Id);

                if (!exists)
                    throw new ArgumentException("Collaboration doesn't exist.");

                var collaboration = _mapper.Map<Collaboration>(collaborationDTO);
                _context.Collaborations.Update(collaboration);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (ArgumentException argE)
            {
                await transaction.RollbackAsync();
                throw new ArgumentException(argE.Message);
            }
            catch (Exception e) { 
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

        public async Task<Guid> AddCollaborationPosition(Guid collaborationId, CreateCollaborationPositionDTO collaborationPositionDTO)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var collaboration = await _context.Collaborations
                    .FirstOrDefaultAsync(c => c.Id == collaborationId);

                if(collaboration is null)
                    throw new ArgumentException("Collaboration doesn't exist.");

                var collaborationPosition = _mapper.Map<CollaborationPosition>(collaborationPositionDTO);

                collaborationPosition.Collaboration = collaboration;
                collaborationPosition.CollaborationId = collaboration.Id;

                var createdEntity = await _context.CollaborationPositions.AddAsync(collaborationPosition);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return createdEntity.Entity.Id;
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

        public async Task DeleteCollaborationPositionById(Guid collaborationId, Guid collaborationPositionId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var deletedRecordsCount = await _context.CollaborationPositions
                    .Where(c => c.Id == collaborationPositionId && c.CollaborationId == collaborationId)
                    .ExecuteDeleteAsync();

                if (deletedRecordsCount == 0)
                    throw new ArgumentException("Collaboration position wasn't deleted. Make sure parameters are correct.");

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

    }
}
