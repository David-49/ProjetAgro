namespace Nomenclatures
{
    public class ProduitFini : Produit
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var cqty in this)
            {
                cqty.Component.Accept(visitor);
            }
        }
    }
}