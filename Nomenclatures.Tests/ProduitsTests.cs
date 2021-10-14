using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nomenclatures.Data;
using NUnit.Framework;

namespace Nomenclatures.Tests
{
    public class ProduitsTests
    {
        [Test]
        public void Sauvegarde()
        {
            var paquetPitchs = new Nomenclatures.Data.ProduitFini()
            {
                Nom = "paquet de pitchs"
            };
            var pitch = new Nomenclatures.Data.ProduitSemiFini();
            var farine = new Nomenclatures.Data.MatierePremiere();
            var chocolat = new Nomenclatures.Data.MatierePremiere();

            paquetPitchs.Composants.Add(new Nomenclatures.Data.ComponentQty { PSF = pitch, Qty = 8});
            pitch.Composants.Add(new Nomenclatures.Data.ComponentQty { MP = farine, Qty = 100});
            pitch.Composants.Add(new Nomenclatures.Data.ComponentQty { MP = chocolat, Qty = 20});

            using var dbContext = new NomenclaturesContext();

            dbContext.Produits.Add(paquetPitchs);
            dbContext.SaveChanges();

            try
            {
                Assert.IsNotNull(dbContext.Produits.FirstOrDefault(p => p.Id == paquetPitchs.Id));

                Assert.AreEqual(1, dbContext.Produits
                    .Where(p => p.Id == paquetPitchs.Id)
                    .Select(p => p.Composants.Count()).First());

                Assert.IsTrue(dbContext.Produits
                    .Where(p => p.Id == paquetPitchs.Id)
                    .SelectMany(p => p.Composants)
                    .Any(c => c.PSF.Id == pitch.Id && c.Qty == 8));

                Assert.IsTrue(dbContext.Produits
                    .Where(p => p.Id == pitch.Id)
                    .SelectMany(p => p.Composants)
                    .Any(c => c.MP.Id == farine.Id
                    && c.Qty == 100));
                Assert.IsTrue(dbContext.Produits
                    .Where(p => p.Id == pitch.Id)
                    .SelectMany(p => p.Composants)
                    .Any(c => c.MP.Id == chocolat.Id
                    && c.Qty == 20));
            }
            finally
            {
                dbContext.MatieresPremieres.Remove(farine);
                dbContext.MatieresPremieres.Remove(chocolat);
                dbContext.Produits.Remove(pitch);
                dbContext.Produits.Remove(paquetPitchs);
                dbContext.SaveChanges();
            }
        }
    }
}