using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IGameObject
    {
        GameObject gameObject { get; }
        Transform transform { get; }
    }
}
