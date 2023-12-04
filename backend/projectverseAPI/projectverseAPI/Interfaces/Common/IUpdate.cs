namespace projectverseAPI.Interfaces.Common
{
    public interface IUpdate<TDto, TResult>
    {
        Task<TResult> Update(TDto entity);
    }
}
