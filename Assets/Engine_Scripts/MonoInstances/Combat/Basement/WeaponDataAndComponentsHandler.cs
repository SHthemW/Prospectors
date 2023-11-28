using Game.Interfaces;
using Game.Services.Combat;
using Game.Services.Physics;
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

        // shoot
        public ShootingRound[] ShootingLoopRound => _staticData.ShootingLoopRound;
        public Vector3 AimingDirection => AimingPosition - MasterPosition;
        public float BulletStartSpeed => _staticData.BulletFlySpeed;
        public float BulletExistTimeSec => _staticData.BulletExistingTime_Sec;
        public float ShootingCdSec => _staticData.ShootingCd_Sec;

        public SingletonComponent<Transform> BulletParent = new("@Bullets");

        IWeaponMaster IWeapon.Master
        {
            get => _master;
            set => _master = value;
        }
    }
}
