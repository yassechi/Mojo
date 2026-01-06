using Microsoft.EntityFrameworkCore;
using core_mojo;             // Pour AppDbContext
using core_mojo.models;      // 'm' minuscule comme dans tes fichiers
using core_mojo.Dtos;        // 'D' majuscule (vérifie si c'est dtos ou Dtos chez toi)
using core_mojo.interfaces;  // 'i' minuscule comme vu dans ton IRepository.cs

namespace infrastructure_mojo.repositories
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
            return await db.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());
        }

        public async Task<User?> GetByName(string name)
        {
            // Recherche par nom de famille 
            return await db.Users.FirstOrDefaultAsync(u => u.LasttName == name);
        }

        public async Task<User> Add(UserAddDto dto)
        {
            User user = new()
            {
                FirstName = dto.FirstName,
                LasttName = dto.LasttName,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role,
                TailleCm = dto.TailleCm ?? 0,
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
            user.Password = dto.Password;
            user.Role = dto.Role;
            user.TailleCm = dto.TailleCm;
            user.IsActif = dto.IsActif;
            user.PhoneNumber = dto.PhoneNumber;

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