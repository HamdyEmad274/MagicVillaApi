using MagicVillaApi.Data;
using MagicVillaApi.Models;
using MagicVillaApi.Repositories.IRepositories;

namespace MagicVillaApi.Repositories
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly AppDbContext villaNumDb;

        public VillaNumberRepository(AppDbContext VillaNumDb) : base(VillaNumDb)
        {
            villaNumDb = VillaNumDb;
        }
        public Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            throw new NotImplementedException();
        }
    }
}
