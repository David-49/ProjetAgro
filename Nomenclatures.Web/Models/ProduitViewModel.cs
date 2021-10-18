using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;

namespace Nomenclatures.Web.Models
{
    public class ProduitViewModel
    {
        public ProduitViewModel() 
        {
            Composants = "[]";
        }

        public ProduitViewModel(Nomenclatures.Data.Produit p)
        {
            Id = p.Id;
            Nom = p.Nom;

            Type = p is Nomenclatures.Data.ProduitFini ? ProductType.ProduitFini : ProductType.ProduitSemiFini;

            Composants = JsonSerializer.Serialize(p.Composants
                .Select(cqty => new 
                { 
                    Idc = cqty.Id, 
                    Qty = cqty.Qty, 
                    Nom = cqty.MP?.Nom ?? cqty.PSF?.Nom, 
                    Id = cqty.MP?.Id ?? cqty.PSF?.Id,
                    Type = cqty.MP != null ? "mp" : "p"
                })
                .ToList());
        }

        public int Id { get; set; }

        public string Nom { get; set; }

        public ProductType Type { get; set; }

        public string Composants { get; set; }

        public Nomenclatures.Data.Produit ToData()
        {
            if (Type == ProductType.ProduitFini)
                return new Nomenclatures.Data.ProduitFini
                {
                    Id = Id,
                    Nom = Nom
                };

            return new Nomenclatures.Data.ProduitSemiFini
            {
                Id = Id,
                Nom = Nom
            };
        }
    }

    public enum  ProductType
    {
        ProduitFini,
        ProduitSemiFini
    }
}