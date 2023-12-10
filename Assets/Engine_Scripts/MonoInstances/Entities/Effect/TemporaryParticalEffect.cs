using Game.Interfaces.GameObj;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Effect
{
    internal sealed class TemporaryParticalEffect : MonoBehaviour, IDestoryManagedObject
    {
        public Action<GameObject> DeactiveAction { get; set; }

        private void OnParticleSystemStopped()
        {
            DeactiveAction.Invoke(gameObject);
        }
    }
}
