using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IMob : IGameObject
    {
        Animator Animator { get; }
        IAnimationStateName AnimStateNames { get; }
    }
}
