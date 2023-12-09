namespace projectverseAPI.Interfaces.Common
{
    public interface IGetById<T>
        where T : class
    {
        Task<T> GetById(Guid id);
    }
}
