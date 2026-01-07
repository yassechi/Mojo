using Microsoft.EntityFrameworkCore;
using core_mojo;             // Pour AppDbContext
using core_mojo.models;      // 'm' minuscule comme dans tes fichiers
using core_mojo.Dtos;        // 'D' majuscule (vérifie si c'est dtos ou Dtos chez toi)
using infrastructure_mojo.Interfaces;
using System.Diagnostics.Contracts;

namespace infrastructure_mojo.repositories
{
    public class ContratRepository : Repository<Contrat>, IContrat
    {
        public ContratRepository(AppDbContext context) : base(context) {}

    }
}