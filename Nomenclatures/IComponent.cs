using System;

namespace Nomenclatures
{
    public interface IComponent
    {
        TimeSpan? DureeOptimaleUtilisation { get; }

        TimeSpan? DureeConservation { get; }

        decimal PrixDeRevient { get; }

        void Accept(IVisitor visitor);
    }
}