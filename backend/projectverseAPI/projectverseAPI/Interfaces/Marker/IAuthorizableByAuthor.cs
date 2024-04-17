using projectverseAPI.Models;

namespace projectverseAPI.Interfaces.Marker
{
    public interface IAuthorizableByAuthor
    {
        User Author { get; set; }
        Guid AuthorId { get; set; }
    }
}
