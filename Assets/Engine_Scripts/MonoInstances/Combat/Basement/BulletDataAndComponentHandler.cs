using Game.Interfaces;
using Game.Utils.Extensions;
using System.Collections;
using UnityEngine;

namespace Game.Instances.Combat
{
    internal sealed class BulletDataAndComponentHandler : MonoBehaviour, IBullet
    {
        [SerializeField]
        private Rigidbody _bulletRb;

        public Rigidbody Rigidbody 
            => _bulletRb.AsSafeInspectorValue(name, rb => rb != null);
        public float MaxExistingSeconds { get; set; }

        // TODO: move to "bullet" folder
    }
}