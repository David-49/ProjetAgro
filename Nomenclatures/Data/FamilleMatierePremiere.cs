using System;

namespace Nomenclatures.Data
{
    public class FamilleMatierePremiere
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public TimeSpan? DureeOptimaleUtilisation { get; set; }
    }
}