using Game.Interfaces;
using Game.Services.Combat;
using Game.Services.Physics;
using Game.Utils.Attributes;
using Game.Utils.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Combat
{
    public sealed class WeaponDataAndComponentsHandler : MonoBehaviour, IWeapon
    {
        [SerializeField]
        private WeaponStaticData_SO _staticData;

        [SerializeField]
        private Transform _muzzle;
        public Transform Muzzle 
            => _muzzle.AsSafeInspectorValue(name, m => m != null);

        private IWeaponMaster _master;

        // from master
        public bool TriggerIsPressing => _master.WantToShoot;
        public Vector3 AimingPosition => _master.AimingPosition;
        public Vector3 MasterPosition => _master.CenterPosition;
        public Vector3 HandlePosition => _master.CurrentHandPositionGetter.Invoke();
        public int TryGetBulletFromMaster() => _master.TryGetBulletFromInventory(MaxMagazineCapacity);

        // shoot
        public ShootingRound[] ShootingLoopRound => _staticData.ShootingLoopRound;
        public Vector3 AimingDirection => AimingPosition - MasterPosition;
        public float BulletAccuracyOffsetAngle 
            => UnityEngine.Random.Range(-_staticData.ShootingAccuracyOffsetAngle, _staticData.ShootingAccuracyOffsetAngle);
        public float BulletStartSpeed => _staticData.BulletSpeed;
        public float BulletExistTimeSec => _staticData.BulletExistingTime_Sec;
        public float ShootingCdSec => _staticData.ShootingRoundCd_Sec;

        public int MaxMagazineCapacity => _staticData.MagazineCapacity;

        [field: SerializeField, ReadOnly]
        public Magazine Magazine { get; set; }


        public SingletonComponent<Transform> BulletParent = new("@Bullets");

        IWeaponMaster IWeapon.Master
        {
            get => _master;
            set => _master = value;
        }
    }
}
