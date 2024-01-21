using Game.Interfaces;
using Game.Services.SAction;
using Game.Utils.Attributes;
using Game.Utils.Collections;
using System;
using UnityEngine;

namespace Game.Instances.Combat
{
    internal sealed class BulletDataAndComponentHandler : MonoBehaviour, IBullet
    {
        private readonly static Checker safe = new(nameof(BulletDataAndComponentHandler));

        [SerializeField]
        private Rigidbody _bulletRb;

        public Rigidbody Rigidbody 
            => safe.Checked(_bulletRb);

        [field: SerializeField, ReadOnly]
        public float CurrentExistingSeconds { get; set; }
        public float MaxExistingSeconds { get; set; }

        [field: SerializeField, ReadOnly]
        public int CurrentHitTimes { get; set; }
        public int MaxHitTimes { get; set; } = 1;

        [field: SerializeField, ReadOnly]
        public int Damage { get; set; }

        [field: SerializeField]
        internal ParameterizedAction[] OnHitActions { get; private set; }
        public Action<GameObject> DeactiveAction { get; set; }

        public SingletonComponent<Transform> HitEffectParent { get; set; } = new("@HitEffects");
        public SingletonComponent<Transform> HitHoleParent { get; set; } = new("@HitHoles");
        
    }
}