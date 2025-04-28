namespace QuickHire.Application.Common.Interfaces.Repository
{
    public interface IRepository
    {
        IQueryable<T>? GetAll<T>() where T: class;
        IQueryable<T>? GetAllReadOnly<T>() where T : class;
        Task<T>? GetByIdAsync<T, TId>(TId id) where T : class;
        Task AddAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
        Task SaveChangesAsync();
    }
}
