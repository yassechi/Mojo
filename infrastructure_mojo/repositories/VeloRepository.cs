using Microsoft.EntityFrameworkCore;
using core_mojo;             // Pour AppDbContext
using core_mojo.models;      // 'm' minuscule comme dans tes fichiers
using core_mojo.Dtos;        // 'D' majuscule (vérifie si c'est dtos ou Dtos chez toi)
using core_mojo.interfaces;  // 'i' minuscule comme vu dans ton IRepository.cs

namespace infrastructure_mojo.repositories
{
    public class VeloRepository : IRepository<Velo, VeloAddDto, VeloUpdateDto>
    {
        private readonly AppDbContext db;

        public VeloRepository(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<List<Velo>> GetAll()
        {
            return await db.Velos.ToListAsync();
        }

        public async Task<Velo?> GetById(int id)
        {
            return await db.Velos.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Velo?> GetByName(string numeroSerie)
        {
            // Pour les vélos, on utilise le numéro de série comme identifiant unique par nom
            return await db.Velos.FirstOrDefaultAsync(v => v.NumeroSerie == numeroSerie);
        }

        public async Task<Velo> Add(VeloAddDto dto)
        {
            Velo velo = new()
            {
                NumeroSerie = dto.NumeroSerie,
                Marque = dto.Marque,
                Modele = dto.Modele,
                PrixAchat = dto.PrixAchat,
                Status = dto.Status
            };

            await db.Velos.AddAsync(velo);
            await db.SaveChangesAsync();
            return velo;
        }

        public async Task<Velo?> Upadte(VeloUpdateDto dto)
        {
            var velo = await db.Velos.FindAsync(dto.Id);

            if (velo == null) return null;

            velo.NumeroSerie = dto.NumeroSerie;
            velo.Marque = dto.Marque;
            velo.Modele = dto.Modele;
            velo.PrixAchat = dto.PrixAchat;
            velo.Status = dto.Status;

            await db.SaveChangesAsync();
            return velo;
        }

        public async Task<bool> Delete(int id)
        {
            var velo = await db.Velos.FindAsync(id);
            if (velo == null) return false;

            db.Velos.Remove(velo);
            await db.SaveChangesAsync();
            return true;
        }
    }
}