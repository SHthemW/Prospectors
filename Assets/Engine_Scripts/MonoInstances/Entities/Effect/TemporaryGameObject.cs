using Game.Interfaces;
using Game.Interfaces.GameObj;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Effect
{
    internal sealed class TemporaryGameObject : MonoBehaviour, IDestoryManagedObject
    {
        [SerializeField] 
        private float _existTime;

        public Action<GameObject> DeactiveAction { get; set; }

        private void OnEnable()
        {
            Delay.Do(() => DeactiveAction?.Invoke(gameObject), _existTime, this);
        }
    }
}
