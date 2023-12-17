using projectverseAPI.Interfaces.Common;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface IComponentService :
        IGetById<List<ProfileComponent>>
    {
        Task<ComponentType> GetComponentTypeByName(string name);
        Task<List<ComponentType>> GetAllComponentTypes();
        Task<ComponentTheme> GetComponentThemeByName(string name);
        Task<List<ComponentTheme>> GetAllComponentThemes();
    }
}
