using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data_mojo.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace data_mojo
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

            // LIEN 1 : L'utilisateur standard (Bénéficiaire)
            modelBuilder.Entity<Contrat>()
                .HasOne(c => c.Beneficiaire)
                .WithMany(u => u.ContratsRecus)
                .HasForeignKey(c => c.BeneficiaireId)
                .OnDelete(DeleteBehavior.Restrict); // On empêche la suppression automatique

            // LIEN 2 : Le Chef (User RH)
            modelBuilder.Entity<Contrat>()
                .HasOne(c => c.UserRH)
                .WithMany(u => u.ContratsGeres)
                .HasForeignKey(c => c.UserRhId)
                .OnDelete(DeleteBehavior.Restrict); // On empêche la suppression automatique


            modelBuilder.Entity<Discussion>()
                    .HasOne(d => d.Client) // ou le nom de votre propriété de navigation
                    .WithMany()
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Restrict); // Désactive le cascade sur le client

            modelBuilder.Entity<Discussion>()
                .HasOne(d => d.Mojo) // ou le nom de votre propriété de navigation
                .WithMany()
                .HasForeignKey(d => d.MojoId)
                .OnDelete(DeleteBehavior.Restrict);
            // --- RELATION MESSAGE (L'erreur actuelle) ---
            // modelBuilder.Entity<Message>()
            //     .HasOne(m => m.User)
            //     .WithMany(u => u.Messages) // On va corriger la liste dans User.cs juste après
            //     .HasForeignKey(m => m.UserId)
            //     .OnDelete(DeleteBehavior.Restrict); // <--- AJOUTE CETTE LIGNE
        }
    }
}