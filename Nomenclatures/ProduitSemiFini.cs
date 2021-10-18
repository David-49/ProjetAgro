using System;
using System.Collections.Generic;
using System.Linq;

namespace Nomenclatures
{
    public class ProduitSemiFini : Produit, IComponent
    {
        public ProduitSemiFini() { }

        public ProduitSemiFini(Nomenclatures.Data.ProduitSemiFini p)
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

        public TimeSpan? DureeOptimaleUtilisation 
        { 
            get
            {
                return this
                    .Select(cqty => cqty.Component)
                    .Min(c => c.DureeOptimaleUtilisation);
            }
        }

        public TimeSpan? DureeConservation 
        { 
            get
            {
                return this
                    .Select(cqty => cqty.Component)
                    .Min(c => c.DureeConservation);
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