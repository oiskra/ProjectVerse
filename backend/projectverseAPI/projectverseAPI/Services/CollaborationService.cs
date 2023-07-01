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

        public async Task CreateCollaboration(Collaboration collaboration)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await _context.Collaborations.AddAsync(collaboration);

                Task.WaitAll(
                    _context.SaveChangesAsync(),
                    transaction.CommitAsync());
            }
            catch { await transaction.RollbackAsync(); }
            ;
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
            var collaboration = await _context.Collaborations.FirstOrDefaultAsync(c => c.Id == collaborationId);
            return collaboration;
        }

        public async Task UpdateCollaboration(Guid collaborationId, Collaboration collaboration)
        {
            if (collaborationId != collaboration.Id) { return; }

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
