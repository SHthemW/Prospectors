using System;
using System.Collections.Generic;

namespace Game.Interfaces
{
    public interface IHoldAnimStateName : IDataHolder
    {
        IAnimationStateName AnimStateNames { get; }
    }
}
