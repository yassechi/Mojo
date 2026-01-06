using Microsoft.EntityFrameworkCore;
using core_mojo;             // Pour AppDbContext
using core_mojo.models;      // 'm' minuscule comme dans tes fichiers
using core_mojo.Dtos;        // 'D' majuscule (vérifie si c'est dtos ou Dtos chez toi)
using core_mojo.interfaces;  // 'i' minuscule comme vu dans ton IRepository.cs

namespace infrastructure_mojo.repositories
{
    public class ContratRepository : IRepository<Contrat, ContratAddDto, ContratUpdateDto>
    {
        private readonly AppDbContext db;

        public ContratRepository(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<List<Contrat>> GetAll()
        {
            // return await db.Contrats.ToListAsync();
            return await db.Contrats.Include(c => c.Velo).ToListAsync();
        }

        public async Task<Contrat?> GetById(int id)
        {
            return await db.Contrats.FirstOrDefaultAsync(c => c.Id == id);
        }

        public  Task<Contrat?> GetByName(string name)
        {
            throw new NotImplementedException("La recherche par nom n'est pas définie pour les contrats.");
        }

        public async Task<Contrat> Add(ContratAddDto contratAddDto)
        {
            Contrat contrat = new()
            {
                DateDebut = contratAddDto.DateDebut,
                DateFin = contratAddDto.DateFin,
                LoyerMensuelHT = contratAddDto.LoyerMensuelHT,
                StatutContrat = contratAddDto.StatutContrat,
                VeloId = contratAddDto.VeloId,
                BeneficiaireId = contratAddDto.BeneficiaireId.ToString(),
                UserRhId = contratAddDto.UserRhId.ToString()
            };

            await db.Contrats.AddAsync(contrat);
            await db.SaveChangesAsync();
            return contrat;
        }

        public async Task<Contrat?> Upadte(ContratUpdateDto contratUpdateDto)
        {
            var contrat = await db.Contrats.FindAsync(contratUpdateDto.Id);

            if (contrat == null)
            {
                return null;
            }

            contrat.DateDebut = contratUpdateDto.DateDebut;
            contrat.DateFin = contratUpdateDto.DateFin;
            contrat.LoyerMensuelHT = contratUpdateDto.LoyerMensuelHT;
            contrat.StatutContrat = contratUpdateDto.StatutContrat;

            await db.SaveChangesAsync();
            return contrat;
        }

        public async Task<bool> Delete(int id)
        {
            var contrat = await db.Contrats.SingleOrDefaultAsync(c => c.Id == id);
            if (contrat is null)
            {
                return false;
            }

            db.Contrats.Remove(contrat);
            await db.SaveChangesAsync();
            return true;
        }
    }
}