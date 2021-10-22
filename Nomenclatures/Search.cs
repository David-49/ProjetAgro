using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nomenclatures
{
    public class Search
    {
        private IQueryable<Nomenclatures.Data.FamilleMatierePremiere> _familles;
        private IQueryable<Nomenclatures.Data.MatierePremiere> _matieres;
        private IQueryable<Nomenclatures.Data.Produit> _produits;

        public Search(IQueryable<Nomenclatures.Data.FamilleMatierePremiere> familles,
            IQueryable<Nomenclatures.Data.MatierePremiere> matieres,
            IQueryable<Nomenclatures.Data.Produit> produits)
        {
            _familles = familles;
            _matieres = matieres;
            _produits = produits;
        }

        public string NomContient { get; set; }

        public bool? EstBio { get; set; }

        public bool InclureMatierePremiere { get; set; } = true;
        
        public bool InclureProduitFini  { get; set; } = true;

        public bool InclureProduitSemiFini { get; set; } = true;

        public bool InclureFamilleMatierePremiere { get; set; } = true;

        public IEnumerable<SearchResult> Execute()
        {
            var query = Enumerable.Empty<SearchResult>();

            if (InclureMatierePremiere)
                query = FiltrerMatieres()
                        .Select(mp => new SearchResult { Id = mp.Id, Nom = mp.Nom, Type = nameof(MatierePremiere) });

            if (InclureProduitFini && InclureProduitSemiFini)
                query = query.Union(FiltrerProduits()
                  .Select(p => new SearchResult { Id = p.Id, Nom = p.Nom, Type = nameof(Produit) }));
            else if (InclureProduitFini)
                query = query.Union(FiltrerProduits().OfType<Nomenclatures.Data.ProduitFini>()
                  .Select(p => new SearchResult { Id = p.Id, Nom = p.Nom, Type = nameof(Produit) }));
            else if (InclureProduitSemiFini)
                query = query.Union(FiltrerProduits().OfType<Nomenclatures.Data.ProduitSemiFini>()
                  .Select(p => new SearchResult { Id = p.Id, Nom = p.Nom, Type = nameof(Produit) }));

            if(InclureFamilleMatierePremiere && !EstBio.HasValue)
                query = query.Union(FiltrerFamilles()
                        .Select(f => new SearchResult { Id = f.Id, Nom = f.Nom, Type = nameof(FamilleMatierePremiere) }));

            return query;
        }

        private IQueryable<Nomenclatures.Data.FamilleMatierePremiere> FiltrerFamilles()
        {
            var query = _familles;
            
            if(!string.IsNullOrEmpty(NomContient))
                query = query.Where(f => f.Nom.Contains(NomContient));

            return query;
        }

        private IQueryable<Nomenclatures.Data.MatierePremiere> FiltrerMatieres()
        {
            var query = _matieres;

            if(!string.IsNullOrEmpty(NomContient))
                query = query.Where(m => m.Nom.Contains(NomContient));

            if(EstBio.HasValue)
                query = query.Where(m => m.Bio == EstBio.Value);

            return query;
        }

        private IQueryable<Nomenclatures.Data.Produit> FiltrerProduits()
        {
            var query = _produits;

            if(!string.IsNullOrEmpty(NomContient))
                query = query.Where(p => p.Nom.Contains(NomContient));

            if(EstBio.HasValue)
                query = query.Where(p => p.Bio == EstBio.Value);

            return query;
        }
    }

    public class SearchResult
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Type { get; set; }
    }
}