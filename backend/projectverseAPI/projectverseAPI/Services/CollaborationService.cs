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

        public async Task<Collaboration> Create(CreateCollaborationRequestDTO collaborationDTO)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var collaboration = _mapper.Map<Collaboration>(collaborationDTO);

                var currentUser = await _authenticationService.GetCurrentUser();
                
                collaboration.Author = currentUser;
                collaboration.AuthorId = Guid.Parse(currentUser.Id);

                var addedCollaboration = await _context.Collaborations.AddAsync(collaboration);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return addedCollaboration.Entity;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

        public async Task Delete(Guid collaborationId)
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

        public async Task<List<Collaboration>> GetAll()
        {
            var collaborations = await _context.Collaborations
                .Include(c => c.Author)
                .Include(c => c.CollaborationPositions)
                .Include(c => c.Technologies)
                .ToListAsync();
            return collaborations;
        }

        public async Task<List<Collaboration>> GetAllByUserId(Guid userId)
        {
            var collaborations = await _context.Collaborations
                .Where(c => c.AuthorId == userId)
                .Include(c => c.Author)
                .Include(c => c.CollaborationPositions)
                .Include(c => c.Technologies)
                .ToListAsync();
            return collaborations;
        }

        public async Task<Collaboration> GetById(Guid collaborationId)
        {
            var collaboration = await _context.Collaborations
                .AsNoTracking()
                .Where(c => c.Id.Equals(collaborationId))
                .Include(c => c.Technologies)
                .Include(c => c.CollaborationPositions)
                .Include(c => c.CollaborationApplicants!)
                    .ThenInclude(ca => ca.ApplicantUser)
                .Include(c => c.Author)
                .FirstOrDefaultAsync();
            
            return collaboration;
        }

        public async Task<Collaboration> Update(UpdateCollaborationRequestDTO dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var collaborationToUpdate = await _context.Collaborations
                    .Where(c => c.Id == dto.Id)
                    .Include(c => c.Author)
                    .Include(c => c.CollaborationPositions)
                    .FirstOrDefaultAsync();

                if (collaborationToUpdate is null)
                    throw new ArgumentException("Collaboration doesn't exist.");

                collaborationToUpdate.Name = dto.Name;
                collaborationToUpdate.Description = dto.Description;
                collaborationToUpdate.Difficulty = (int)dto.Difficulty!;
                collaborationToUpdate.Technologies = dto.Technologies;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return collaborationToUpdate;
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
    }
}
