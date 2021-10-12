using System;

namespace Nomenclatures
{
    public class DLCCalculator : IVisitor
    {
        private TimeSpan _dureeConservationMin = TimeSpan.FromDays(365*9000);

        void IVisitor.Visit(ProduitFini pf)
        {

        }

        void IVisitor.Visit(ProduitSemiFini psf)
        {

        }

        void IVisitor.Visit(MatierePremiere mp)
        {
            if(mp.DureeConservation.HasValue &&
                mp.DureeConservation < _dureeConservationMin)
                _dureeConservationMin = mp.DureeConservation.Value;
        }

        void IVisitor.Visit(FamilleMatierePremiere fmp)
        {

        }

        public DateTime CalculerDLC(DateTime dateFabrication, ProduitFini p)
        {
            _dureeConservationMin = TimeSpan.FromDays(365*9000);
            p.Accept(this);
            return dateFabrication + _dureeConservationMin;
        }

        public DateTime CalculerDLC(DateTime dateFabrication, ProduitSemiFini psf)
        {
            _dureeConservationMin = TimeSpan.FromDays(365*9000);
            psf.Accept(this);
            return dateFabrication + _dureeConservationMin;
        }
    }
}