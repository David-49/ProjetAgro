using Microsoft.EntityFrameworkCore;

namespace Nomenclatures
{
    public class NomenclaturesContext : DbContext
    {
        public DbSet<MatierePremiere> MatieresPremieres { get; set; }

        public DbSet<FamilleMatierePremiere> FamillesPremieres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Nomenclatures;Trusted_connection=True;");
        }
    }
}