using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IHoldCharAnimator : IDataHolder
    {
        Animator Animator { get; }
    }
}
