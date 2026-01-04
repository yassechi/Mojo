using data_mojo.dtos;
using data_mojo.models;
using Microsoft.EntityFrameworkCore;
using data_mojo.interfaces;

namespace data_mojo.repositories
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
            return await db.Contrats.ToListAsync();
        }

        public async Task<Contrat?> GetById(int id)
        {
            return await db.Contrats.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Contrat?> GetByName(string name)
        {
            // Note: Si le contrat n'a pas de propriété 'Name', 
            // vous pouvez adapter cette recherche (ex: par numéro de contrat)
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
                BeneficiaireId = contratAddDto.BeneficiaireId,
                UserRhId = contratAddDto.UserRhId
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