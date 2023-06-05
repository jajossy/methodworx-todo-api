namespace TodoApplication.Repository
{
    public interface IRepository
    {
        Task<T> CreateAsync<T>(T entity) where T : class;
        Task<T> CreateAsync<T>(T entity, CancellationToken cancellationToken) where T : class;
        Task<T> DeleteAsync<T>(T entity) where T : class;
        Task<T> DeleteAsync<T>(T entity, CancellationToken cancellationToken) where T : class;
        Task<T> PatchAsync<T>(T entity) where T : class;
        Task<T> PatchAsync<T>(T entity, CancellationToken cancellationToken) where T : class;
        IQueryable<T> Query<T>() where T : class;
    }
}
