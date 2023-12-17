using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Services
{
    public class ComponentService : IComponentService
    {
        private readonly ProjectVerseContext _context;

        public ComponentService(
            ProjectVerseContext context)
        {
            _context = context;
        }

        public Task<List<ProfileComponent>> GetById(Guid id)
        {
            
        }

        public async Task<List<ComponentTheme>> GetAllComponentThemes()
        {
            var themes = await _context.ComponentThemes
                .ToListAsync();

            return themes;
        }

        public async Task<List<ComponentType>> GetAllComponentTypes()
        {
            var types = await _context.ComponentTypes
                .ToListAsync();

            return types;
        }

        public async Task<ComponentTheme> GetComponentThemeByName(string name)
        {
            var theme = await _context.ComponentThemes
                .Where(t => t.Name == name)
                .FirstOrDefaultAsync();

            if (theme is null)
                throw new ArgumentException("Theme doesn't exist.");

            return theme;
        }

        public async Task<ComponentType> GetComponentTypeByName(string name)
        {
            var type = await _context.ComponentTypes
                .Where(t => t.Name == name)
                .FirstOrDefaultAsync();

            if (type is null)
                throw new ArgumentException("Type doesn't exist.");

            return type;
        }


    }
}
