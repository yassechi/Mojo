using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_mojo.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace core_mojo
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Amortissement> Amortissements { get; set; }
        public DbSet<Contrat> Contrats { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        // public DbSet<User> Users { get; set; }
        public DbSet<Velo> Velos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Bénéficiaire
            modelBuilder.Entity<Contrat>()
                .HasOne(c => c.Beneficiaire)
                .WithMany(u => u.ContratsRecus)
                .HasForeignKey(c => c.BeneficiaireId)
                .OnDelete(DeleteBehavior.Restrict); // On empêche la suppression automatique

            // Manager
            modelBuilder.Entity<Contrat>()
                .HasOne(c => c.UserRH)
                .WithMany(u => u.ContratsGeres)
                .HasForeignKey(c => c.UserRhId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Discussion>()
                    .HasOne(d => d.Client)
                    .WithMany()
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Discussion>()
                .HasOne(d => d.Mojo) 
                .WithMany()
                .HasForeignKey(d => d.MojoId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}