namespace projectverseAPI.Interfaces.Common
{
    public interface ICreateRelated<TDto, TResult>
    {
        Task<TResult> CreateRelated(Guid relatedId, TDto dto);
    }
}
