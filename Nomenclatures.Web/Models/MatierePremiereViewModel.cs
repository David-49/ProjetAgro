using System.Collections;
using System.Collections.Generic;

namespace Nomenclatures.Web.Models
{
    public class MatierePremiereViewModel : Nomenclatures.Data.MatierePremiere
    {
        public MatierePremiereViewModel(Nomenclatures.Data.MatierePremiere mp, IEnumerable<Nomenclatures.Data.FamilleMatierePremiere> familles)
        {
            Id = mp.Id;
            Nom = mp.Nom;
            Description = mp.Description;
            DureeConservation = mp.DureeConservation;
            Famille = mp.Famille;
            FamilleId = mp.FamilleId;
            PoidsUnitaire = mp.PoidsUnitaire;
            PourcentageHumidite = mp.PourcentageHumidite;
            Bio = mp.Bio;
            PrixUnitaire = mp.PrixUnitaire;

            Familles = familles;
        }

        public MatierePremiereViewModel(IEnumerable<Nomenclatures.Data.FamilleMatierePremiere> familles)
        {
            Familles = familles;
        }

        public IEnumerable<Nomenclatures.Data.FamilleMatierePremiere> Familles{ get; }
    }
}