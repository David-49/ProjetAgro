using System;

namespace Nomenclatures
{
    public class MatierePremiere : IComponent
    {
        public string Nom { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }

        public int PourcentageHumidite { get; set; }

        public double PoidsUnitaire { get; set; }

        public TimeSpan? DureeConservation { get; set; }

        public TimeSpan? DureeOptimaleUtilisation 
        { 
            get
            {
                if (Famille != null) return Famille.DureeOptimaleUtilisation;
                return null;
            }
        }

        public FamilleMatierePremiere Famille { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            if (Famille != null) Famille.Accept(visitor);
        }
    }
}