namespace HotelListingAPI.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int? id);
        Task<T> GetByIdAsync(int? id);
        Task<List<T>> GetAllAsync();
    }
}
