using data_mojo.dtos;
using data_mojo.models;
using Microsoft.EntityFrameworkCore;
using data_mojo.interfaces;

namespace data_mojo.repositories
{
    public class DiscussionRepository : IRepository<Discussion, DiscussionAddDto, DiscussionUpdateDto>
    {
        private readonly AppDbContext db;

        public DiscussionRepository(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<List<Discussion>> GetAll()
        {
            return await db.Discussions.ToListAsync();
        }

        public async Task<Discussion?> GetById(int id)
        {
            return await db.Discussions.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Discussion?> GetByName(string name)
        {
            // Recherche par l'objet de la discussion
            return await db.Discussions.FirstOrDefaultAsync(d => d.Objet == name);
        }

        public async Task<Discussion> Add(DiscussionAddDto discussionAddDto)
        {
            Discussion discussion = new()
            {
                Objet = discussionAddDto.Objet,
                Status = discussionAddDto.Status,
                DateCreation = discussionAddDto.DateCreation,
                UserId = discussionAddDto.UserId
            };

            await db.Discussions.AddAsync(discussion);
            await db.SaveChangesAsync();
            return discussion;
        }

        public async Task<Discussion?> Upadte(DiscussionUpdateDto discussionUpdateDto)
        {
            var discussion = await db.Discussions.FindAsync(discussionUpdateDto.Id);

            if (discussion == null) return null;

            discussion.Objet = discussionUpdateDto.Objet;
            discussion.Status = discussionUpdateDto.Status;
            discussion.DateCreation = discussionUpdateDto.DateCreation;

            await db.SaveChangesAsync();
            return discussion;
        }

        public async Task<bool> Delete(int id)
        {
            var discussion = await db.Discussions.FindAsync(id);
            if (discussion == null) return false;

            db.Discussions.Remove(discussion);
            await db.SaveChangesAsync();
            return true;
        }
    }
}