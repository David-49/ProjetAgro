using NUnit.Framework;
using System.Linq;

namespace Nomenclatures.Tests
{
    public class CompositionTests
    {
        [Test]
        public void Composition_d_un_produit_fini()
        {
            var paquetPitchs = new ProduitFini();
            var pitch = new ProduitSemiFini();
            var farine = new MatierePremiere();
            var chocolat = new MatierePremiere();

            paquetPitchs.Add(pitch, 8);
            pitch.Add(farine, 100);
            pitch.Add(chocolat, 20);

            Assert.AreEqual(1, paquetPitchs.Count());
            Assert.AreEqual(2, pitch.Count());

            Assert.IsTrue(paquetPitchs.Any(c => c.Component == pitch 
                && c.Qty == 8));

            Assert.IsTrue(pitch.Any(c => c.Component == farine 
                && c.Qty == 100));
            Assert.IsTrue(pitch.Any(c => c.Component == chocolat 
                && c.Qty == 20));
        }
    }
}