using data_mojo.dtos;
using data_mojo.models;
using Microsoft.EntityFrameworkCore;
using data_mojo.interfaces;

namespace data_mojo.repositories
{
    public class AccessoireRepository : IRepository<Accessoire, AccessoireAddDto, AccessoireUpdateDto>
    {
        private readonly AppDbContext db;

        public AccessoireRepository(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<List<Accessoire>> GetAll()
        {
            return await db.Accessoires.ToListAsync();
        }

        public async Task<Accessoire?> GetById(int id)
        {
            return await db.Accessoires.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Accessoire?> GetByName(string name)
        {
            return await db.Accessoires.FirstOrDefaultAsync(a => a.Name == name);
        }

        public async Task<Accessoire> Add(AccessoireAddDto accessoireAddDto)
        {
            Accessoire accessoire = new()
            {
                Name = accessoireAddDto.Name,
                Price = accessoireAddDto.Price,
                Stock = accessoireAddDto.Stock
            };

            await db.Accessoires.AddAsync(accessoire);
            await db.SaveChangesAsync();
            return accessoire;
        }

        public async Task<Accessoire?> Upadte(AccessoireUpdateDto accessoireUpdateDto)
        {
            var accessoire = await db.Accessoires.FindAsync(accessoireUpdateDto.Id);

            if (accessoire == null)
            {
                return null;
            }

            accessoire.Name = accessoireUpdateDto.Name;
            accessoire.Price = accessoireUpdateDto.Price;
            accessoire.Stock = accessoireUpdateDto.Stock;
            await db.SaveChangesAsync();
            return accessoire;
        }

        public async Task<bool> Delete(int id)
        {
            var accessoire = await db.Accessoires.SingleOrDefaultAsync(a => a.Id == id);
            if (accessoire is null)
            {
                return false;
            }
            db.Remove(accessoire);
            db.SaveChanges();
            return true;
        }
    }
}