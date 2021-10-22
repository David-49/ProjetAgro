using System;

namespace Nomenclatures.Web.Models
{
    public class SearchResultViewModel : SearchResult
    {
        public SearchResultViewModel(SearchResult sr)
        {
            Id = sr.Id;
            Nom = sr.Nom;
            Type = sr.Type;
        }

        public string EditUrl { get; set; }
    }  
}