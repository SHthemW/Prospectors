using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IHoldCharRigidbody : IDataHolder
    {
        Rigidbody Rigidbody { get; }
    }
}
