using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Nomenclatures
{
    public class ProduitFini : Produit
    {
        public ProduitFini()
        {
            AddRule(new ReglePrix().Valider);
        }

        public ProduitFini(Nomenclatures.Data.ProduitFini p)
            : this()
        {
            Id = p.Id;
            Nom = p.Nom;
            Description = p.Description;
            Bio = p.Bio;
            PrixUnitaire = p.PrixUnitaire;

            foreach(var composant in p.Composants)
            {
                if(composant.PSF != null)
                {
                    Add(new ProduitSemiFini(composant.PSF), composant.Qty);
                }    
                else if(composant.MP != null)
                {
                    Add(new MatierePremiere(composant.MP), composant.Qty);
                }         
                else
                    throw new NotImplementedException();
            }
        }

        public decimal PrixUnitaire { get; set; }

        protected override double GetPoidsTotal()
        {
            return new PoidsCalculateur().Caculer(this);
        }

        protected override double GetPoidsBio()
        {
            return new PoidsCalculateur(true).Caculer(this);
        }

        public void Accept(IVisitor visitor)
        {
            foreach (var cqty in this)
            {
                cqty.Component.Accept(visitor);
            }
            visitor.Visit(this);
        }
    }
}