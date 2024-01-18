using Game.Instances.Player;
using Game.Interfaces;
using Game.Services.Combat;
using Game.Utils.Attributes;
using Game.Utils.Collections;
using Game.Utils.Extensions;
using System.Linq;
using UnityEngine;

namespace Game.Instances.Combat
{
    public sealed class WeaponDataAndComponentsHandler : MonoBehaviour, IWeapon
    {
        private readonly static Checker safe = new(nameof(WeaponDataAndComponentsHandler));

        [SerializeField]
        private WeaponStaticData_SO _staticData;

        /*
         *  Weapon part objects
         */

        [SerializeField]
        private Transform _muzzle;
        public Transform Muzzle 
            => safe.Checked(_muzzle);

        [SerializeField]
        private Transform _shellThrowingWindow;
        public Transform ShellThrowingWindow
            => safe.Checked(_shellThrowingWindow, fatal: false);

        [field: SerializeField, ReadOnly]
        public Magazine Magazine { get; set; }

        [SerializeField]
        private Animator _animator;
        public Animator Animator
            => safe.Checked(_animator, fatal: false);

        public SingletonComponent<Transform> BulletParent { get; set; } = new("@Bullets");
        public SingletonComponent<Transform> ShellParent { get; set; } = new("@Shells");

        /*
         *  Master properties
         */

        private IWeaponMaster _master;

        IWeaponMaster IWeapon.Master
        {
            get => _master;
            set => _master = value;
        }
        public bool TriggerIsPressing => _master.WantToShoot;
        public Vector3 AimingPosition => _master.AimingPosition;
        public Vector3 MasterPosition => _master.CenterPosition;
        public Vector3 HandlePosition => _master.CurrentHandPositionGetter.Invoke();
        public Animator[] MasterAnimators => _master.CharacterAnimators;
        public int MaxBulletNumberFromMaster => _master.TryGetBulletFromInventory(MaxMagazineCapacity);


        [field: Header("Runtimes")]

        [field: SerializeField, ReadOnly]
        public ShootingRound[] ShootingLoopRound { get; private set; }

        public Vector3 AimingDirection => AimingPosition - MasterPosition;
        public float BulletAccuracyOffsetAngle 
            => UnityEngine.Random.Range(-_staticData.ShootingAccuracyOffsetAngle, _staticData.ShootingAccuracyOffsetAngle);
        public float BulletStartSpeed => _staticData.BulletSpeed;
        public float BulletExistTimeSec => _staticData.BulletExistingTime_Sec;
        public float ShootingCdSec => _staticData.ShootingRoundCd_Sec;
        public int MaxMagazineCapacity => _staticData.MagazineCapacity;
        public int BulletDamage => _staticData.BulletDamage;

        private void Awake()
        {
            ShootingLoopRound = _staticData.ShootingLoopRound.Select(round => round.DeepClone()).ToArray();
        }
    }
}
