using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface IAuthorizableByAuthor
    {
        User Author { get; set; }
        Guid AuthorId { get; set; }
    }
}
