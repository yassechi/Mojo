using Microsoft.EntityFrameworkCore;
using infrastructure_mojo.Interfaces;
using core_mojo.models;
using core_mojo;

namespace infrastructure_mojo.repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext db;

        public Repository(AppDbContext _db)
        {
            db = _db;
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await db.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetById(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> Add(T entity)
        {
            await db.Set<T>().AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T?> Upadte(T entity)
        {
            db.Set<T>().Update(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> Delete(int id)
        {
            var obj = await GetById(id);
            if (obj is null)
            {
                return false;
            }
            db.Set<T>().Remove(obj);
            await db.SaveChangesAsync();
            return true;
        }
    }
}