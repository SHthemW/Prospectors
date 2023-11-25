using Game.Utils.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Combat
{
    [Serializable]
    public struct ShootingRound
    {
        [SerializeField]
        private GameObject _bullet;
        public readonly GameObject Bullet 
            => _bullet.AsSafeInspectorValue(nameof(ShootingRound), static b => b != null);

        [SerializeField, Tooltip("子弹实际发射出的角度与瞄准角度的偏移量. 默认为0.")]
        private float _shootingAngleOffset;
        public readonly float ShootingAngleOffset 
            => _shootingAngleOffset;
    }
}
