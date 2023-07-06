using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Services
{
    public class CollaborationService : ICollaborationService
    {
        private readonly ProjectVerseContext _context;

        public CollaborationService(ProjectVerseContext context)
        {
            _context = context;
        }

        public async Task<Collaboration?> CreateCollaboration(Collaboration collaboration)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var addedCollaboration = await _context.Collaborations.AddAsync(collaboration);


                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return addedCollaboration.Entity;
            }
            catch 
            { 
                await transaction.RollbackAsync();
                return null;
            }
            
        }

        public async Task DeleteCollaborationById(Guid collaborationId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var collaborationToDelete = await _context.Collaborations.FirstOrDefaultAsync(c => c.Id == collaborationId);
                if (collaborationToDelete == null) { return; }

                _context.Collaborations.Remove(collaborationToDelete);

                Task.WaitAll(
                    _context.SaveChangesAsync(),
                    transaction.CommitAsync());
            }
            catch { await transaction.RollbackAsync(); }
           
        }

        public async Task<List<Collaboration>> GetAllCollaborations()
        {
            var collaborations = await _context.Collaborations.ToListAsync();
            return collaborations;
        }

        public async Task<Collaboration?> GetCollaborationById(Guid collaborationId)
        {
            var collaboration = await _context.Collaborations
                            .Where(c => c.Id.Equals(collaborationId))
                            .Include(c => c.CollaborationPositions)
                            .Include(c => c.Author)
                            .FirstOrDefaultAsync(c => c.Id == collaborationId);
            
            return collaboration;
        }

        public async Task UpdateCollaboration(Collaboration collaboration)
        {

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Collaborations.Update(collaboration);

                Task.WaitAll(
                    _context.SaveChangesAsync(),
                    transaction.CommitAsync());
            }
            catch { await transaction.RollbackAsync(); }
        }
    }
}
