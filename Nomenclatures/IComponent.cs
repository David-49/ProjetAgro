using System;

namespace Nomenclatures
{
    public interface IComponent
    {
        TimeSpan? DureeOptimaleUtilisation { get; }

        TimeSpan? DureeConservation { get; }
    }
}