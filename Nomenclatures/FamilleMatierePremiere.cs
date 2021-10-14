using System;

namespace Nomenclatures
{
    public class FamilleMatierePremiere
    {
        public int Id { get; set; }
        
        public TimeSpan? DureeOptimaleUtilisation { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }  
    }
}