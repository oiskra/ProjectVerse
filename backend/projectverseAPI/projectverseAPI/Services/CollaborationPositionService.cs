using AutoMapper;
using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Services
{
    public class CollaborationPositionService : ICollaborationPositionService
    {
        private readonly ProjectVerseContext _context;
        private readonly IMapper _mapper;

        public CollaborationPositionService(
            ProjectVerseContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CollaborationPosition> CreateRelated(Guid relatedId, CreateCollaborationPositionDTO dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var collaboration = await _context.Collaborations
                    .FirstOrDefaultAsync(c => c.Id == relatedId);

                if (collaboration is null)
                    throw new ArgumentException("Collaboration doesn't exist.");

                var collaborationPosition = _mapper.Map<CollaborationPosition>(dto);

                collaborationPosition.Collaboration = collaboration;
                collaborationPosition.CollaborationId = collaboration.Id;

                var createdEntity = await _context.CollaborationPositions.AddAsync(collaborationPosition);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return createdEntity.Entity;
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

        public async Task Delete(Guid id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var deletedRecordsCount = await _context.CollaborationPositions
                    .Where(c => c.Id == id)
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

        public async Task<CollaborationPosition> Update(UpdateCollaborationPositionDTO dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var collaborationPositionToUpdate = await _context.CollaborationPositions
                    .FirstOrDefaultAsync(cp => cp.Id == dto.Id);

                if (collaborationPositionToUpdate is null)
                    throw new ArgumentException("Collaboration position doesn't exist");

                collaborationPositionToUpdate.Name = dto.Name!;
                collaborationPositionToUpdate.Description = dto.Description!;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return collaborationPositionToUpdate;
            }
            catch (ArgumentException)
            {
                await transaction.RollbackAsync();
                throw;
            }
            

            

        }
    }
}
