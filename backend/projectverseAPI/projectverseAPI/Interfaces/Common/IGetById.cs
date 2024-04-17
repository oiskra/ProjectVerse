namespace projectverseAPI.Interfaces.Common
{
    public interface IGetById<T>
    {
        Task<T> GetById(Guid id);
    }
}
