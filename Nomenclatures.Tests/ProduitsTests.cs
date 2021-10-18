using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Nomenclatures.Data;
using NUnit.Framework;

namespace Nomenclatures.Tests
{
    public class ProduitsTests
    {
        [Test, Ignore("A corriger")]
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

        [Test]
        public void Validite_Produit_bio()
        {
            var paquetPitchs = new ProduitFini()
            {
                Nom = "paquet de pitchs",
                Bio = true
            };
            var pitch = new ProduitSemiFini{ Bio = true };
            var farine = new MatierePremiere { Bio = true, PoidsUnitaire = 1 };
            var chocolat = new MatierePremiere { Bio = true, PoidsUnitaire = 1 };

            paquetPitchs.Add(pitch, 8);
            pitch.Add(farine, 100);
            pitch.Add(chocolat, 20);

            Assert.IsTrue(paquetPitchs.IsValid);
            Assert.AreEqual(0, paquetPitchs.GetErrors().Count());
        }

        [Test]
        public void Non_Validite_Produit_bio()
        {
            var paquetPitchs = new ProduitFini()
            {
                Nom = "paquet de pitchs",
                Bio = true
            };
            var pitch = new ProduitSemiFini{ Bio = true };
            var farine = new MatierePremiere { Bio = false, PoidsUnitaire = 1 };
            var chocolat = new MatierePremiere { Bio = false, PoidsUnitaire = 1 };

            paquetPitchs.Add(pitch, 8);
            pitch.Add(farine, 100);
            pitch.Add(chocolat, 20);

            Assert.IsFalse(paquetPitchs.IsValid);
            Assert.AreEqual(1, paquetPitchs.GetErrors().Count());

            var messageErreur = paquetPitchs.GetErrors().First();
            Assert.AreEqual("Bio", messageErreur.Property);
            Assert.AreEqual("Poids des matières premières bio insuffisant", messageErreur.Message);
        }
    }
}