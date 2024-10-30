using MagicVillaApi.Models;
using System.Linq.Expressions;

namespace MagicVillaApi.Repositories.IRepositories
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> UpdateAsync(Villa entity);
    }
}
