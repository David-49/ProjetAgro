using System.Collections.Generic;

namespace Nomenclatures
{
    public interface IComposite : IEnumerable<ComponentQty>
    {
        void Add(IComponent component, double qty, Unit unit);

        void Remove(IComponent component);
    }

    public enum Unit
    {
        Gram,
        Piece
    }
}