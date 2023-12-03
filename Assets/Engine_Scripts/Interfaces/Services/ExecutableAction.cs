using System.Collections;
using UnityEngine;

namespace Game.Interfaces
{
    public abstract class ExecutableAction : ScriptableObject
    {
        public abstract void Execute(object caster);
    }
}