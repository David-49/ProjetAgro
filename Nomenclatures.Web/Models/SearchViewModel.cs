namespace Nomenclatures.Web.Models
{
    public class SearchViewModel
    {
        public string NomContient { get; set; }

        public bool? EstBio { get; set; }

        public bool InclureMatierePremiere { get; set; } = true;
        
        public bool InclureProduitFini  { get; set; } = true;

        public bool InclureProduitSemiFini { get; set; } = true;

        public bool InclureFamilleMatierePremiere { get; set; } = true;
    }
}