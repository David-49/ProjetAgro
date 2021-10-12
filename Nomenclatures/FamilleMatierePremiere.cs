using System;

namespace Nomenclatures
{
    public class FamilleMatierePremiere
    {
        public TimeSpan? DureeOptimaleUtilisation { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }  
    }
}