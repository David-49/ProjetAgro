using System;
using System.ComponentModel.DataAnnotations;

namespace Nomenclatures.Data
{
    public class MatierePremiere
    {
        public MatierePremiere() { }
        
        public MatierePremiere(Nomenclatures.MatierePremiere mp)
        {
            Nom = mp.Nom;
            Id = mp.Id;
            Description = mp.Description;
            PourcentageHumidite = mp.PourcentageHumidite;
            PoidsUnitaire = mp.PoidsUnitaire;
            DureeConservation = mp.DureeConservation;
            if(mp.Famille != null)
                Famille = new FamilleMatierePremiere
                {
                    Id = mp.Famille.Id,
                    DureeOptimaleUtilisation = mp.Famille.DureeOptimaleUtilisation
                };
        }

        [Required]
        public string Nom { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }

        public int PourcentageHumidite { get; set; }

        public double PoidsUnitaire { get; set; }

        public TimeSpan? DureeConservation { get; set; }

        public FamilleMatierePremiere Famille { get; set; }

        public int? FamilleId { get; set; }
    }
}