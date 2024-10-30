using MagicVillaApi.Data;
using MagicVillaApi.Models;
using MagicVillaApi.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVillaApi.Repositories
{
    public class VillaRepository :Repository<Villa>, IVillaRepository
    {
        private readonly AppDbContext db;

        public VillaRepository(AppDbContext db): base(db)
        {
            this.db = db;
        }
        

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.DateUpdated = DateTime.Now;
            db.Update(entity);
            await db.SaveChangesAsync();
            return entity;
        }
    }
}
