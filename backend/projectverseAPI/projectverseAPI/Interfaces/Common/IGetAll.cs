namespace projectverseAPI.Interfaces.Common
{
    public interface IGetAll<T>
        where T : class
    {
        Task<List<T>> GetAll(string? searchTerm);
    }
}
