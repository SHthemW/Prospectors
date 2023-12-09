using Game.Interfaces;
using Game.Utils.Attributes;
using Game.Utils.Extensions;
using UnityEngine;

namespace Game.Instances.Combat
{
    internal sealed class BulletDataAndComponentHandler : MonoBehaviour, IBullet
    {
        [SerializeField]
        private Rigidbody _bulletRb;

        public Rigidbody Rigidbody 
            => _bulletRb.AsSafeInspectorValue(name, rb => rb != null);

        [field: SerializeField, ReadOnly]
        public float MaxExistingSeconds { get; set; }

        [field: SerializeField, ReadOnly]
        public int CurrentHitTimes { get; set; }
        public int MaxHitTimes { get; set; } = 1;

        // TODO: move to "bullet" folder
    }
}