using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Nomenclatures.Tests
{
    public class MatieresPremieresTests
    {
        [Test]
        public void Sauvegarde()
        {
            var mp = new MatierePremiere
            {
                Nom = "farine",
                Description = "",
                PoidsUnitaire = 1,
                DureeConservation = TimeSpan.FromHours(3),
                Famille = new FamilleMatierePremiere
                {
                    DureeOptimaleUtilisation = TimeSpan.FromHours(2)
                }
            };

            using var dbContext = new NomenclaturesContext();

            dbContext.MatieresPremieres.Add(mp);
            dbContext.SaveChanges();

            var mpLu = dbContext.MatieresPremieres
                .Include(m => m.Famille)
                .FirstOrDefault(m => m.Nom == "farine");

            dbContext.MatieresPremieres.Remove(mpLu);
            dbContext.SaveChanges();
            
            Assert.IsNotNull(mpLu);
            Assert.AreEqual(mp.Nom, mpLu.Nom);
            Assert.AreEqual(mp.PoidsUnitaire, mpLu.PoidsUnitaire);
            Assert.AreEqual(mp.Famille.DureeOptimaleUtilisation,
                mpLu.Famille.DureeOptimaleUtilisation);            
        }
    }

}