using Microsoft.EntityFrameworkCore;
using core_mojo;             // Pour AppDbContext
using core_mojo.models;      // 'm' minuscule comme dans tes fichiers
using core_mojo.Dtos;        // 'D' majuscule (vérifie si c'est dtos ou Dtos chez toi)
using core_mojo.interfaces;  // 'i' minuscule comme vu dans ton IRepository.cs

namespace infrastructure_mojo.repositories
{
    public class MessageRepository : IRepository<Message, MessageAddDto, MessageUpdateDto>
    {
        private readonly AppDbContext db;

        public MessageRepository(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<List<Message>> GetAll()
        {
            return await db.Messages.ToListAsync();
        }

        public async Task<Message?> GetById(int id)
        {
            return await db.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Message?> GetByName(string contentPart)
        {
            // Recherche par un extrait du contenu du message
            return await db.Messages.FirstOrDefaultAsync(m => m.Contenu.Contains(contentPart));
        }

        public async Task<Message> Add(MessageAddDto dto)
        {
            Message message = new()
            {
                Contenu = dto.Contenu,
                DateEnvoi = dto.DateEnvoi,
                DiscussionId = dto.DiscussionId
            };

            await db.Messages.AddAsync(message);
            await db.SaveChangesAsync();
            return message;
        }

        public async Task<Message?> Upadte(MessageUpdateDto dto)
        {
            var message = await db.Messages.FindAsync(dto.Id);

            if (message == null) return null;

            message.Contenu = dto.Contenu;
            message.DateEnvoi = dto.DateEnvoi;

            await db.SaveChangesAsync();
            return message;
        }

        public async Task<bool> Delete(int id)
        {
            var message = await db.Messages.FindAsync(id);
            if (message == null) return false;

            db.Messages.Remove(message);
            await db.SaveChangesAsync();
            return true;
        }
    }
}