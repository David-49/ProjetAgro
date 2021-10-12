using System;
using System.Linq;

namespace Nomenclatures
{
    public class ProduitSemiFini : Produit, IComponent
    {
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
    }
}