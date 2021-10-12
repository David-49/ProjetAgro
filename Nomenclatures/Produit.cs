using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nomenclatures
{
    public abstract class Produit : IComposite
    {
        private List<ComponentQty> _components = new List<ComponentQty>();
        private string _nom;

        public string Nom 
        { 
            get { return _nom; }
            set { _nom = value; }
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime? CalculerDLUO(DateTime dateFabrication)
        {
            var dluo = new DateTime(9999, 12, 31);

            foreach (var composant in this)
            {
                if (composant.Component is MatierePremiere mp 
                    && mp.Famille != null && mp.Famille.DureeOptimaleUtilisation.HasValue)
                {
                    var d = dateFabrication + mp.Famille.DureeOptimaleUtilisation.Value;
                    if (d < dluo) dluo = d;
                }
                else if (composant.Component is ProduitSemiFini psf)
                {
                    var d = psf.CalculerDLUO(dateFabrication);
                    if (d.HasValue && d < dluo) dluo = d.Value;
                }
            }

            if (dluo.Year == 9999) return null;

            return dluo;
        }

        public DateTime? CalculerDLC(DateTime dateFabrication)
        {
            var dlc = new DateTime(9999, 12, 31);

            foreach (var composant in this)
            {
                if (composant.Component is MatierePremiere mp && mp.DureeConservation.HasValue)
                {
                    var d = dateFabrication + mp.DureeConservation.Value;
                    if (d < dlc) dlc = d;
                }
                else if (composant.Component is ProduitSemiFini psf)
                {
                    var d = psf.CalculerDLC(dateFabrication);
                    if (d.HasValue && d < dlc) dlc = d.Value;
                }
            }

            if (dlc.Year == 9999) return null;

            return dlc;
        }

        public void Add(IComponent component, double qty, Unit unit)
        {
            _components.Add(new ComponentQty
            {
                Component = component,
                Qty = qty,
                Unit = unit
            });
        }

        public void Remove(IComponent component)
        {
            _components.Remove(_components.First(c => c.Component == component));
        }

        public IEnumerator<ComponentQty> GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}