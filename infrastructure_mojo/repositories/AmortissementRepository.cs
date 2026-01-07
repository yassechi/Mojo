using Microsoft.EntityFrameworkCore;
using core_mojo;             // Pour AppDbContext

using core_mojo.Dtos;        // 'D' majuscule (vérifie si c'est dtos ou Dtos chez toi)
using infrastructure_mojo.Interfaces;
using core_mojo.models;

namespace infrastructure_mojo.repositories
{
    public class AmortissementRepository : Repository<Amortissement>, IAmortissement
    {
        public AmortissementRepository(AppDbContext context) : base(context) {}
  
    }
}