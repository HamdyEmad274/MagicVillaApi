using MagicVillaApi.Models;
using System.Linq.Expressions;

namespace MagicVillaApi.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
