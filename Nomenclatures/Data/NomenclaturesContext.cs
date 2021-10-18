using System;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

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

        public NomenclaturesContext()
            : base(new DbContextOptionsBuilder<NomenclaturesContext>()
                .UseNpgsql(@"Server=127.0.0.1;Port=5432;Database=Agro;User Id=postgres;Password=ov^*bD!8;")
                .Options)
        { }

        public NomenclaturesContext(DbContextOptions<NomenclaturesContext> options)
            : base(options)
        {}

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