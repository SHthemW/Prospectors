using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public abstract class ScriptableAction : ScriptableObject
    {
        public abstract dynamic Execute(params dynamic[] args);
    }
}
