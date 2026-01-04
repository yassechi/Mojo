using data_mojo.dtos;
using data_mojo.models;
using Microsoft.EntityFrameworkCore;
using data_mojo.interfaces;

namespace data_mojo.repositories
{
    public class UserRepository : IRepository<User, UserAddDto, UserUpdateDto>
    {
        private readonly AppDbContext db;

        public UserRepository(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<List<User>> GetAll()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByName(string name)
        {
            // Recherche par nom de famille (LasttName)
            return await db.Users.FirstOrDefaultAsync(u => u.LasttName == name);
        }

        public async Task<User> Add(UserAddDto dto)
        {
            User user = new()
            {
                FirstName = dto.FirstName,
                LasttName = dto.LasttName,
                Email = dto.Email,
                Hpassword = dto.Hpassword,
                Role = dto.Role,
                TailleCm = dto.TailleCm,
                IsActif = dto.IsActif,
                OrganisationId = dto.OrganisationId
            };

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<User?> Upadte(UserUpdateDto dto)
        {
            var user = await db.Users.FindAsync(dto.Id);

            if (user == null) return null;

            user.FirstName = dto.FirstName;
            user.LasttName = dto.LasttName;
            user.Email = dto.Email;
            user.Hpassword = dto.Hpassword;
            user.Role = dto.Role;
            user.TailleCm = dto.TailleCm;
            user.IsActif = dto.IsActif;

            await db.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await db.Users.FindAsync(id);
            if (user == null) return false;

            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return true;
        }
    }
}