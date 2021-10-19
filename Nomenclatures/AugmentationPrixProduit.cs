using Microsoft.EntityFrameworkCore;
using Nomenclatures.Data;

namespace Nomenclatures
{
    public class AugmentationPrixProduit : Command
    {
        private readonly NomenclaturesContext _dbContext;

        public AugmentationPrixProduit(NomenclaturesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int PourcentageAugmentation { get; set; }

        public override void Execute()
        {
            _dbContext.Database.ExecuteSqlInterpolated(@$"UPDATE Produits 
                SET PrixUnitaire = PrixUnitaire + PrixUnitaire * {PourcentageAugmentation} / 100
                WHERE PrixUnitaire IS NOT NULL");
        }
    }
}