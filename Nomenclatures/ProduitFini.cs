using System;

namespace Nomenclatures
{
    public class ProduitFini : Produit
    {
        public ProduitFini() { }

        public ProduitFini(Nomenclatures.Data.ProduitFini p)
        {
            Id = p.Id;
            Nom = p.Nom;
            Description = p.Description;
            Bio = p.Bio;

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