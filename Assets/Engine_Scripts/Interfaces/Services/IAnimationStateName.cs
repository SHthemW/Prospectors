using System;
using System.Collections.Generic;

namespace Game.Interfaces
{
    public interface IAnimationStateName
    {
        string CurrentVelocity { get; }
        string OnHit { get; }
    }
}
