using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IMob
    {
        int CurrentHealth { get; set; }
        int MaxHealth { get; }

        Animator Animator { get; }
        IAnimationStateName AnimNames { get; }
    }
}
