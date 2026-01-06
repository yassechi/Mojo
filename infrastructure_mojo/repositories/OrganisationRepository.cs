using Microsoft.EntityFrameworkCore;
using core_mojo;             // Pour AppDbContext
using core_mojo.models;      // 'm' minuscule comme dans tes fichiers
using core_mojo.Dtos;        // 'D' majuscule (vérifie si c'est dtos ou Dtos chez toi)
using core_mojo.interfaces;  // 'i' minuscule comme vu dans ton IRepository.cs

namespace infrastructure_mojo.repositories
{
    public class OrganisationRepository : IRepository<Organisation, OrganisationAddDto, OrganisationUpdateDto>
    {
        private readonly AppDbContext db;

        public OrganisationRepository(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<List<Organisation>> GetAll()
        {
            return await db.Organisations.ToListAsync();
        }

        public async Task<Organisation?> GetById(int id)
        {
            return await db.Organisations.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Organisation?> GetByName(string name)
        {
            return await db.Organisations.FirstOrDefaultAsync(o => o.Name == name);
        }

        public async Task<Organisation> Add(OrganisationAddDto dto)
        {
            Organisation organisation = new()
            {
                Name = dto.Name,
                Code = dto.Code,
                Address = dto.Address,
                ContactEmail = dto.ContactEmail,
                IsActif = dto.IsActif
            };

            await db.Organisations.AddAsync(organisation);
            await db.SaveChangesAsync();
            return organisation;
        }

        public async Task<Organisation?> Upadte(OrganisationUpdateDto dto)
        {
            var organisation = await db.Organisations.FindAsync(dto.Id);

            if (organisation == null) return null;

            organisation.Name = dto.Name;
            organisation.Code = dto.Code;
            organisation.Address = dto.Address;
            organisation.ContactEmail = dto.ContactEmail;
            organisation.IsActif = dto.IsActif;

            await db.SaveChangesAsync();
            return organisation;
        }

        public async Task<bool> Delete(int id)
        {
            var organisation = await db.Organisations.FindAsync(id);
            if (organisation == null) return false;

            db.Organisations.Remove(organisation);
            await db.SaveChangesAsync();
            return true;
        }
    }
}