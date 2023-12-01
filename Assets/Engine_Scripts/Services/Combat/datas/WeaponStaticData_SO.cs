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
        private float _shootingCd_Sec;
        public float ShootingCd_Sec
            => _shootingCd_Sec.AsSafeInspectorValue(name, static cd => cd > 0);

        [SerializeField]
        private float _bulletSpeed;
        public float BulletSpeed
            => _bulletSpeed.AsSafeInspectorValue(name, static s => s > 0);

        [SerializeField]
        private float _bulletExistingTime_Sec;
        public float BulletExistingTime_Sec
            => _bulletExistingTime_Sec.AsSafeInspectorValue(name, t => t > 0);

        [SerializeField]
        private ShootingRound[] _shootingLoopRound;
        public ShootingRound[] ShootingLoopRound 
            => _shootingLoopRound.AsSafeInspectorValue(name, static r => r != null && r.Length > 0);
    }
}
