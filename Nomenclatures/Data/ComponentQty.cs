namespace Nomenclatures.Data
{
    public class ComponentQty
    {
        public int Id { get; set; }

        public double Qty { get; set; }

        public MatierePremiere MP { get; set; }

        public ProduitSemiFini PSF { get; set; }
    }
}