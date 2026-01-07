using infrastructure_mojo.Interfaces;
using Microsoft.EntityFrameworkCore;
using core_mojo.models;
using core_mojo.Dtos;
using core_mojo;

namespace infrastructure_mojo.repositories
{
    public class UserRepository : Repository<User>, IUser
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> GetByName(string name)
        {
            return await db.Set<User>().FirstOrDefaultAsync(u => u.FirstName == name || u.LasttName == name);
        }

        public virtual async Task<User?> GetById2(string id)
        {
            return await db.Set<User>().FindAsync(id);
        }
    }
}