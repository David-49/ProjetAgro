namespace Nomenclatures
{
    public class ReglePrix
    {
        public ErrorMessage Valider(DomainObject domainObject)
        {
            if (domainObject is ProduitFini pf)
            {
                if (pf.PrixUnitaire < pf.PrixDeRevient)
                    return new ErrorMessage { Property = "PrixUnitaire", Message = "Prix unitaire du produit fini inférieur au prix de revient" };
            }

            return null;
        }
    }
}