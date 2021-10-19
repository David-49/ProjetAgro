using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Nomenclatures.Data
{
    public class ProduitFini : Produit
    {
        public ProduitFini() { }

        public ProduitFini(Nomenclatures.ProduitFini pf)
        {
            Id = pf.Id;
            Nom = pf.Nom;
            Description = pf.Description;
            Bio = pf.Bio;
            PrixUnitaire = pf.PrixUnitaire;

            foreach(var composant in pf)
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

        public decimal PrixUnitaire { get; set; }
    }
}