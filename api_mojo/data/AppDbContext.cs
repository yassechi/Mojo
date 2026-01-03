using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api_mojo.data.models;

namespace api_mojo.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Accessoire> Accessoires { get; set;}
        public DbSet<Amortissement> Amortissements { get; set;}
        public DbSet<Contrat_Accessoire> Contrat_Accessoires { get; set;}
        public DbSet<Contrat> Contrats { get; set;}
        public DbSet<Discussion> Discussions { get; set;}
        public DbSet<Intervention> Interventions { get; set;}
        public DbSet<Message> Messages { get; set;}
        public DbSet<Organisation> Organisations { get; set;}
        public DbSet<User> Users { get; set;}
        public DbSet<Velo> Velos { get; set;}
    }
}