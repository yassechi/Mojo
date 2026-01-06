using Microsoft.EntityFrameworkCore;
using core_mojo;             // Pour AppDbContext
using core_mojo.models;      // 'm' minuscule comme dans tes fichiers
using core_mojo.Dtos;        // 'D' majuscule (vérifie si c'est dtos ou Dtos chez toi)
using core_mojo.interfaces;  // 'i' minuscule comme vu dans ton IRepository.cs

namespace infrastructure_mojo.repositories
{
    public class AmortissementRepository : IRepository<Amortissement, AmortissmentAddDto, AmortissmentUpdateDto>
    {
        private readonly AppDbContext db;
        public AmortissementRepository(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<List<Amortissement>> GetAll()
        {
            return await db.Amortissements.ToListAsync();
        }

        public async Task<Amortissement?> GetById(int id)
        {
            return await db.Amortissements.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Amortissement?> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Amortissement> Add(AmortissmentAddDto amortDto)
        {
            Amortissement amortissement = new()
            {
                DateDebut = amortDto.DateDebut,
                ValeurInit = amortDto.ValeurInit,
                DureeMois = amortDto.DureeMois,
                ValeurResiduelleFinale = amortDto.ValeurResiduelleFinale,
                VeloId = amortDto.VeloId
            };
            await db.Amortissements.AddAsync(amortissement);
            db.SaveChanges();
            return amortissement;
        }

        public async Task<Amortissement?> Upadte(AmortissmentUpdateDto amortissementDto)
        {
            var amortissement = await db.Amortissements.SingleOrDefaultAsync(a => a.Id == amortissementDto.Id);
            if (amortissement is null)
            {
                return null;
            }
            amortissement.DateDebut = amortissementDto.DateDebut;
            amortissement.ValeurInit = amortissementDto.ValeurInit;
            amortissement.DureeMois = amortissementDto.DureeMois;
            amortissement.ValeurResiduelleFinale = amortissementDto.ValeurResiduelleFinale;
            db.SaveChanges();
            return amortissement;
        }

        public async Task<bool> Delete(int id)
        {
            var amortissement = await db.Amortissements.SingleOrDefaultAsync(a => a.Id == id);
            if (amortissement is null)
            {
                return false;
            }
            db.Remove(amortissement);
            db.SaveChanges();
            return true;
        }
    }
}