using System.Collections.Generic;
using System.Linq;

namespace Nomenclatures
{
    public class PoidsCalculateur : IVisitor
    {
        private Stack<double> _poids = new Stack<double>();

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