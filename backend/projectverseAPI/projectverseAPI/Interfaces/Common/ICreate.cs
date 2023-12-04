namespace projectverseAPI.Interfaces.Common
{
    public interface ICreate<TDto, TResult>
    {
        Task<TResult> Create(TDto entity);
    }
}
