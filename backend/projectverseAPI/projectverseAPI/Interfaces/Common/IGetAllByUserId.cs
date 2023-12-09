namespace projectverseAPI.Interfaces.Common
{
    public interface IGetAllByUserId<T>
        where T : class
    {
        Task<List<T>> GetAllByUserId(Guid userId);
    }
}
