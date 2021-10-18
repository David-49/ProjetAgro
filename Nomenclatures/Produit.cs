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

        public bool Bio { get; set; }

        public bool IsValid
        {
            get
            {
                var poids = GetPoidsTotal();
                var poidsBio = GetPoidsBio();

                return !Bio || poidsBio >= poids * 90 / 100;
            }
        }

        public IEnumerable<ErrorMessage> GetErrors()
        {
            if (!IsValid)
            {
                yield return new ErrorMessage
                {
                    Property = "Bio",
                    Message = "Poids des matières premières bio insuffisant"
                };
            }
        }

        public DateTime? CalculerDLUO(DateTime dateFabrication)
        {
            var dluo = new DateTime(9999, 12, 31);

            foreach (var composant in this)
            {
                if (composant.Component.DureeOptimaleUtilisation.HasValue)
                {
                    var d = dateFabrication + composant.Component.DureeOptimaleUtilisation.Value;
                    if (d < dluo) dluo = d;
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
                if (composant.Component.DureeConservation.HasValue)
                {
                    var d = dateFabrication + composant.Component.DureeConservation.Value;
                    if (d < dlc) dlc = d;
                }
            }

            if (dlc.Year == 9999) return null;

            return dlc;
        }

        public void Add(IComponent component, double qty)
        {
            _components.Add(new ComponentQty
            {
                Component = component,
                Qty = qty
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

        protected abstract double GetPoidsTotal();

        protected abstract double GetPoidsBio();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}