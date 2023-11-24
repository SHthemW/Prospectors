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

        [SerializeField] 
        private GameObject _bulletPrefeb;
        public GameObject BulletPrefeb
            => _bulletPrefeb.AsSafeInspectorValue(name, static o => o != null);

        [Header("Shooting")]

        [SerializeField]
        private int _damage;
        public int Damage 
            => _damage.AsSafeInspectorValue(name, static d => d > 0);

        [SerializeField]
        private bool _enableBulletGravity;
        public bool EnableBulletGravity
            => _enableBulletGravity;

        [SerializeField]
        private float _bulletFlySpeed;
        public float BulletFlySpeed
            => _bulletFlySpeed.AsSafeInspectorValue(name, static s => s > 0);

        // TODO: shooting round def
    }
}
