using MagicVillaApi.Models;

namespace MagicVillaApi.Repositories.IRepositories
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateAsync(VillaNumber entity);
    }
}
