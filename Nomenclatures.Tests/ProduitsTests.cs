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

            var paquetPitchsLu = dbContext.Produits
                .Include(p => p.Composants)
                    .ThenInclude(cqty => cqty.PSF)
                        .ThenInclude(p => p.Composants)
                            .ThenInclude(cqty => cqty.PSF)
                .Include(p => p.Composants)
                    .ThenInclude(cqty => cqty.PSF)
                        .ThenInclude(p => p.Composants)
                            .ThenInclude(cqty => cqty.MP)
                .Include(p => p.Composants)
                    .ThenInclude(cqty => cqty.MP)
                .FirstOrDefault(p => p.Nom == "paquet de pitchs");
            Assert.IsNotNull(paquetPitchsLu);

            Assert.AreEqual(1, paquetPitchs.Composants.Count());
            Assert.AreEqual(2, pitch.Composants.Count());

            Assert.IsTrue(paquetPitchs.Composants.Any(c => c.PSF == pitch 
                && c.Qty == 8));

            Assert.IsTrue(pitch.Composants.Any(c => c.MP == farine 
                && c.Qty == 100));
            Assert.IsTrue(pitch.Composants.Any(c => c.MP == chocolat 
                && c.Qty == 20));
        }
    }
}