using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Nomenclatures.Data;
using NUnit.Framework;

namespace Nomenclatures.Tests
{
    public class ProduitsFinisTests
    {
        [Test]
        public void Validite_Prix_Unitaire()
        {
            var paquetPitchs = new ProduitFini()
            {
                Nom = "paquet de pitchs",
                PrixUnitaire = 1000
            };
            var pitch = new ProduitSemiFini();
            var farine = new MatierePremiere { PrixUnitaire = 1 };
            var chocolat = new MatierePremiere { PrixUnitaire = 1 };

            paquetPitchs.Add(pitch, 8);
            pitch.Add(farine, 100);
            pitch.Add(chocolat, 20);

            Assert.IsTrue(paquetPitchs.IsValid);
            Assert.AreEqual(0, paquetPitchs.GetErrors().Count());
        }

        [Test]
        public void Non_Validite_Prix_Unitaire()
        {
            var paquetPitchs = new ProduitFini()
            {
                Nom = "paquet de pitchs",
                PrixUnitaire = 5
            };
            var pitch = new ProduitSemiFini();
            var farine = new MatierePremiere { PrixUnitaire = 1 };
            var chocolat = new MatierePremiere { PrixUnitaire = 1 };

            paquetPitchs.Add(pitch, 8);
            pitch.Add(farine, 100);
            pitch.Add(chocolat, 20);

            Assert.IsFalse(paquetPitchs.IsValid);
            Assert.AreEqual(1, paquetPitchs.GetErrors().Count());

            var messageErreur = paquetPitchs.GetErrors().First();
            Assert.AreEqual("PrixUnitaire", messageErreur.Property);
            Assert.AreEqual("Prix unitaire du produit fini inférieur au prix de revient", messageErreur.Message);
        }
    }
}