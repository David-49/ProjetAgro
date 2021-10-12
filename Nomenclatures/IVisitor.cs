namespace Nomenclatures
{
    public interface IVisitor
    {
        void Visit(ProduitFini pf);

        void Visit(ProduitSemiFini psf);

        void Visit(MatierePremiere mp);

        void Visit(FamilleMatierePremiere fmp);
    }
}