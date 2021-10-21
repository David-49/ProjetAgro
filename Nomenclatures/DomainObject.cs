using System.Collections;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using Nomenclatures;

namespace Nomenclatures
{
    public abstract class DomainObject
    {
        private List<GetErrorDelegate> _rules = new List<GetErrorDelegate>();

        public bool IsValid
        {
            get
            {
                foreach (var rule in _rules)
                {
                    if( rule(this) != null) return false;
                }

                return true;
            }
        }

        public IEnumerable<ErrorMessage> GetErrors()
        {
            foreach (var rule in _rules)
            {
                var em = rule(this);
                if(em != null) yield return em;
            }
        }

        protected void AddRule(GetErrorDelegate errorDelegate)
        {
            _rules.Add(errorDelegate);
        }
    }

    public delegate ErrorMessage GetErrorDelegate(DomainObject domainObject);
}