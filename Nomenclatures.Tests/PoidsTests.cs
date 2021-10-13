using System;
using System.Linq;
using NUnit.Framework;

namespace Nomenclatures.Tests
{
    public class PoidsTests
    {
        [Test]
        public void Poids()
        {
            var paquetPitchs = new ProduitFini();
            var pitch = new ProduitSemiFini();
            var farine = new MatierePremiere();
            var chocolat = new MatierePremiere();

            farine.PoidsUnitaire = 1;
            farine.PourcentageHumidite = 50;
            chocolat.PoidsUnitaire = 1;

            paquetPitchs.Add(pitch, 8);
            pitch.Add(farine, 100);
            pitch.Add(chocolat, 20);

            var calcPoids = new PoidsCalculateur();
            Assert.AreEqual(70, calcPoids.Caculer(pitch));
            Assert.AreEqual(8 * 70, calcPoids.Caculer(paquetPitchs));
        }
    }
}