using System;
using Microsoft.EntityFrameworkCore;

namespace Nomenclatures.Data
{
    public class NomenclaturesContext : DbContext
    {
        public DbSet<MatierePremiere> MatieresPremieres { get; set; }

        public DbSet<FamilleMatierePremiere> FamillesPremieres { get; set; }

        public DbSet<Produit> Produits { get; set; }

        public DbSet<ProduitFini> ProduitsFinis { get; set; }

        public DbSet<ProduitSemiFini> ProduitsSemiFinis { get; set; }

        public DbSet<ComponentQty> Composants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Nomenclatures2;Trusted_connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ComponentQty>()
                .HasOne<ProduitSemiFini>(cqty => cqty.PSF);
            modelBuilder.Entity<ComponentQty>()
                .HasOne<MatierePremiere>(cqty => cqty.MP);

                modelBuilder.Entity<Produit>()
                    .HasMany(p => p.Composants)
                    .WithOne(c => c.Compose);
        }
    }
}