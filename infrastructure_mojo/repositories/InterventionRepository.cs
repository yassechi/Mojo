using Microsoft.EntityFrameworkCore;
using core_mojo;             // Pour AppDbContext
using core_mojo.models;      // 'm' minuscule comme dans tes fichiers
using core_mojo.Dtos;        // 'D' majuscule (vérifie si c'est dtos ou Dtos chez toi)
using core_mojo.interfaces;  // 'i' minuscule comme vu dans ton IRepository.cs

namespace infrastructure_mojo.repositories
{
    public class InterventionRepository : IRepository<Intervention, InterventionAddDto, InterventionUpdateDto>
    {
        private readonly AppDbContext db;

        public InterventionRepository(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<List<Intervention>> GetAll()
        {
            return await db.Interventions.ToListAsync();
        }

        public async Task<Intervention?> GetById(int id)
        {
            return await db.Interventions.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Intervention?> GetByName(string name)
        {
            // Les interventions n'ont généralement pas de "Nom", on cherche ici dans la description
            return await db.Interventions.FirstOrDefaultAsync(i => i.Description.Contains(name));
        }

        public async Task<Intervention> Add(InterventionAddDto dto)
        {
            Intervention intervention = new()
            {
                DateIntervention = dto.DateIntervention,
                TypeIntervention = dto.TypeIntervention,
                Description = dto.Description,
                Cout = dto.Cout,
                VeloId = dto.VeloId
            };

            await db.Interventions.AddAsync(intervention);
            await db.SaveChangesAsync();
            return intervention;
        }

        public async Task<Intervention?> Upadte(InterventionUpdateDto dto)
        {
            var intervention = await db.Interventions.FindAsync(dto.Id);

            if (intervention == null) return null;

            intervention.DateIntervention = dto.DateIntervention;
            intervention.TypeIntervention = dto.TypeIntervention;
            intervention.Description = dto.Description;
            intervention.Cout = dto.Cout;

            await db.SaveChangesAsync();
            return intervention;
        }

        public async Task<bool> Delete(int id)
        {
            var intervention = await db.Interventions.FindAsync(id);
            if (intervention == null) return false;

            db.Interventions.Remove(intervention);
            await db.SaveChangesAsync();
            return true;
        }
    }
}