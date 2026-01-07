using infrastructure_mojo.Interfaces;
using Microsoft.EntityFrameworkCore;
using core_mojo.models;
using core_mojo.Dtos;
using core_mojo;


namespace infrastructure_mojo.repositories
{
    public class OrganisationRepository : Repository<Organisation>, IOrganisation
    {
        public OrganisationRepository(AppDbContext context) : base(context) { }

        public async Task<Organisation?> GetByName(string name)
        {
            return await db.Set<Organisation>()
                           .FirstOrDefaultAsync(o => o.Name == name);
        }
    }
}