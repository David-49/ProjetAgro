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