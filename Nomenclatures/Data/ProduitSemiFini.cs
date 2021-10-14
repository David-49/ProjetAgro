using System;

namespace Nomenclatures.Data
{
    public class ProduitSemiFini : Produit
    {
        public ProduitSemiFini() { }

        public ProduitSemiFini(Nomenclatures.ProduitSemiFini p)
        {
            Id = p.Id;
            Nom = p.Nom;
            Description = p.Description;

            foreach(var composant in p)
            {
                if(composant.Component is Nomenclatures.ProduitSemiFini psf)
                {
                    Composants.Add(new ComponentQty
                    {
                        Qty = composant.Qty,
                        PSF = new ProduitSemiFini(psf)
                    });
                }    
                else if(composant.Component is Nomenclatures.MatierePremiere mp)
                {
                    Composants.Add(new ComponentQty
                    {
                        Qty = composant.Qty,
                        MP = new MatierePremiere(mp)
                    });
                }         
                else
                    throw new NotImplementedException();
            }
        }
    }
}