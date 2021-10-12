using NUnit.Framework;
using System;
using System.Linq;

namespace Nomenclatures.Tests
{
    public class DatesLimitesTests
    {
        [Test]
        public void DLC()
        {
            var paquetPitchs = new ProduitFini();
            var pitch = new ProduitSemiFini();
            var farine = new MatierePremiere();
            var chocolat = new MatierePremiere();

            paquetPitchs.Add(pitch, 8, Unit.Piece);
            pitch.Add(farine, 100, Unit.Gram);
            pitch.Add(chocolat, 20, Unit.Gram);

            farine.DureeConservation = TimeSpan.FromDays(30);
            chocolat.DureeConservation = TimeSpan.FromDays(10);

            var dateFabrication = new DateTime(2021, 10, 12);

            Assert.AreEqual(new DateTime(2021, 10, 22), paquetPitchs.CalculerDLC(dateFabrication));
            Assert.AreEqual(new DateTime(2021, 10, 22), pitch.CalculerDLC(dateFabrication));
        }

        [Test]
        public void DLUO()
        {
            var paquetPitchs = new ProduitFini();
            var pitch = new ProduitSemiFini();
            var farine = new MatierePremiere();
            var chocolat = new MatierePremiere();

            paquetPitchs.Add(pitch, 8, Unit.Piece);
            pitch.Add(farine, 100, Unit.Gram);
            pitch.Add(chocolat, 20, Unit.Gram);

            farine.Famille = new FamilleMatierePremiere
            {
                DureeOptimaleUtilisation = TimeSpan.FromDays(30)
            };
            chocolat.Famille = new FamilleMatierePremiere
            {
                DureeOptimaleUtilisation = TimeSpan.FromDays(10)
            };

            var dateFabrication = new DateTime(2021, 10, 12);

            Assert.AreEqual(new DateTime(2021, 10, 22), paquetPitchs.CalculerDLUO(dateFabrication));
            Assert.AreEqual(new DateTime(2021, 10, 22), pitch.CalculerDLUO(dateFabrication));
        }

        [Test]
        public void DLCAvecVisitor()
        {
            var paquetPitchs = new ProduitFini();
            var pitch = new ProduitSemiFini();
            var farine = new MatierePremiere();
            var chocolat = new MatierePremiere();

            paquetPitchs.Add(pitch, 8, Unit.Piece);
            pitch.Add(farine, 100, Unit.Gram);
            pitch.Add(chocolat, 20, Unit.Gram);

            farine.DureeConservation = TimeSpan.FromDays(30);
            chocolat.DureeConservation = TimeSpan.FromDays(10);

            var dateFabrication = new DateTime(2021, 10, 12);

            var dlcCalc = new DLCCalculator();
            Assert.AreEqual(new DateTime(2021, 10, 22), dlcCalc.CalculerDLC(dateFabrication, paquetPitchs));
            Assert.AreEqual(new DateTime(2021, 10, 22), dlcCalc.CalculerDLC(dateFabrication, pitch));
        }
    }
}