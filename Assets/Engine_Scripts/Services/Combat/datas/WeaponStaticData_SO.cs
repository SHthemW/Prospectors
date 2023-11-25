using Game.Interfaces;
using Game.Utils.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Combat
{
    [CreateAssetMenu(fileName = "new Weapon", menuName = "Data/Combat/Weapon")]
    public sealed class WeaponStaticData_SO : ScriptableObject
    {
        [Header("Assets")]

        [SerializeField]
        private GameObject _prefeb;
        public GameObject Prefeb
            => _prefeb.AsSafeInspectorValue(name, static p => p != null && p.TryGetComponent<IWeapon>(out _));

        [Header("Shooting")]

        [SerializeField]
        private int _damage;
        public int Damage 
            => _damage.AsSafeInspectorValue(name, static d => d > 0);

        [SerializeField]
        private float _shootingCd_Sec;
        public float ShootingCd_Sec
            => _shootingCd_Sec.AsSafeInspectorValue(name, static cd => cd > 0);

        [SerializeField]
        private bool _enableBulletGravity;
        public bool EnableBulletGravity
            => _enableBulletGravity;

        [SerializeField]
        private float _bulletFlySpeed;
        public float BulletFlySpeed
            => _bulletFlySpeed.AsSafeInspectorValue(name, static s => s > 0);

        [SerializeField]
        private List<ShootingRound> _shootingLoopRound;
        public List<ShootingRound> ShootingLoopRound 
            => _shootingLoopRound.AsSafeInspectorValue(name, static r => r != null && r.Count > 0);
    }
}
