using System.Collections.Generic;
using System.Linq;

namespace Nomenclatures
{
    public class PoidsCalculateur : IVisitor
    {
        private Stack<double> _poids = new Stack<double>();
        private bool _onlyBio;

        public PoidsCalculateur()
        {
            _onlyBio = false;
        }

        public PoidsCalculateur(bool onlyBio)
        {
            _onlyBio = onlyBio;
        }

        void IVisitor.Visit(ProduitFini pf)
        {
            Visit(pf);
        }

        void IVisitor.Visit(ProduitSemiFini psf)
        {
            Visit(psf);
        }

        void IVisitor.Visit(MatierePremiere mp)
        {
            if(_onlyBio && !mp.Bio)
                _poids.Push(0);
            else
                _poids.Push(mp.PoidsUnitaire - mp.PoidsUnitaire * mp.PourcentageHumidite / 100);
        }

        void IVisitor.Visit(FamilleMatierePremiere fmp)
        {

        }

        private void Visit(Produit p)
        {
            double poids = 0;

            foreach (var cpqty in p.Reverse())
            {
                poids += _poids.Pop() * cpqty.Qty;
            }

            _poids.Push(poids);
        }

        public double Caculer(ProduitFini p)
        {
            _poids.Clear();
            p.Accept(this);
            return _poids.Pop();
        }

        public double Caculer(ProduitSemiFini p)
        {
            _poids.Clear();
            p.Accept(this);
            return _poids.Pop();
        }
    }
}