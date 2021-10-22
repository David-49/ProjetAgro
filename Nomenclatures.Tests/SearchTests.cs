using System;
using System.Collections.Generic;
using System.Linq;
using Nomenclatures.Data;
using NUnit.Framework;

namespace Nomenclatures.Tests
{
    public class SearchTests
    {
        private List<Nomenclatures.Data.FamilleMatierePremiere> _familles;
        private List<Nomenclatures.Data.MatierePremiere> _matieresPremieres;
        private List<Nomenclatures.Data.Produit> _produits;

        [Test]
        public void SimpleSearch()
        {
            var search = new Search(
                _familles.AsQueryable(), 
                _matieresPremieres.AsQueryable(),
                _produits.AsQueryable())
            {
                NomContient = "i"
            };

            var result = search.Execute();

            Assert.AreEqual(6, result.Count());
            Assert.IsFalse(result.Any(r => r.Nom == "Chocolat"));
        }

        [Test]
        public void SearchForBio()
        {
            var search = new Search(
                _familles.AsQueryable(), 
                _matieresPremieres.AsQueryable(),
                _produits.AsQueryable())
            {
                EstBio = true
            };

            var result = search.Execute();

            Assert.AreEqual(3, result.Count());
            Assert.IsFalse(result.Any(r => r.Nom == "famille farine"));
        }

        [Test]
        public void SearchByType()
        {
            var search = new Search(
                _familles.AsQueryable(), 
                _matieresPremieres.AsQueryable(),
                _produits.AsQueryable())
            {
                InclureMatierePremiere = true,
                InclureProduitFini = true,
                InclureProduitSemiFini = false,
                InclureFamilleMatierePremiere = false
            };

            var result = search.Execute();

            Assert.AreEqual(4, result.Count());
            Assert.IsFalse(result.Any(r => r.Nom == "famille farine"));
        }

        [Test]
        public void SearchByTypeOnDb()
        {
            using var dbContext = new NomenclaturesContext();
            Assert.DoesNotThrow(() => new Search(dbContext.FamillesPremieres, dbContext.MatieresPremieres, dbContext.Produits)
            {
                InclureMatierePremiere = true,
                InclureProduitFini = true,
                InclureProduitSemiFini = false,
                InclureFamilleMatierePremiere = false,
                EstBio = true,
                NomContient = "i"
            }.Execute().ToList());
        }

        [SetUp]
        public void Setup()
        {
            _familles = new List<Nomenclatures.Data.FamilleMatierePremiere>()
            {
                new Data.FamilleMatierePremiere() { Id = 1, Nom = "famille farine" },
                new Data.FamilleMatierePremiere() { Id = 2, Nom = "famille arôme" }
            };

            _matieresPremieres = new List<Nomenclatures.Data.MatierePremiere>()
            {
                new Data.MatierePremiere() { Id = 1, Nom = "Farine bio", Bio = true},
                new Data.MatierePremiere() { Id = 2, Nom = "Farine", Bio = false},
                new Data.MatierePremiere() { Id = 3, Nom = "Chocolat", Bio = false},
            };

            _produits = new List<Nomenclatures.Data.Produit>()
            {
                new Data.ProduitFini { Id = 1, Nom = "Paquet de pitchs", Bio = true },
                new Data.ProduitSemiFini { Id = 2, Nom = "Pitch", Bio = true }
            };
        }
    }
}