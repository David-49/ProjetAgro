namespace Nomenclatures
{
    public class ProduitFini : Produit
    {
        public void Accept(IVisitor visitor)
        {
            foreach (var cqty in this)
            {
                cqty.Component.Accept(visitor);
            }
            visitor.Visit(this);
        }
    }
}