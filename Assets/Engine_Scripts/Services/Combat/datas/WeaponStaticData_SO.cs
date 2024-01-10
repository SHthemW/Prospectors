using Game.Interfaces;
using Game.Utils.Extensions;
using UnityEngine;

namespace Game.Services.Combat
{
    [CreateAssetMenu(fileName = "new Weapon", menuName = "Data/Combat/Weapon")]
    public sealed class WeaponStaticData_SO : ScriptableObject
    {
        private readonly static Checker safe = new (nameof(WeaponStaticData_SO));

        [Header("Assets")]

        [SerializeField]
        private GameObject _prefeb;
        public GameObject Prefeb
            => safe.Checked(_prefeb, valid: static p => p != null && p.TryGetComponent<IWeapon>(out _));

        [Header("Property")]

        [SerializeField]
        private int _damage;
        public int BulletDamage 
            => safe.Checked(_damage);

        [Header("Shooting")]

        [SerializeField]
        private float _bulletSpeed;
        public float BulletSpeed
            => safe.Checked(_bulletSpeed);

        [SerializeField]
        private float _bulletExistingTime_Sec;
        public float BulletExistingTime_Sec
            => safe.Checked(_bulletExistingTime_Sec);

        [SerializeField]
        private int _magazineCapacity;
        public int MagazineCapacity
            => safe.Checked(_magazineCapacity);

        [Space]

        [SerializeField]
        private float _shootingRoundCd_Sec;
        public float ShootingRoundCd_Sec
            => safe.Checked(_shootingRoundCd_Sec);

        [SerializeField]
        private float _shootingAccuracyOffsetAngle;
        public float ShootingAccuracyOffsetAngle
            => _shootingAccuracyOffsetAngle;

        [Space]

        [SerializeField]
        private ShootingRound[] _shootingLoopRound;
        public ShootingRound[] ShootingLoopRound 
            => safe.Checked(_shootingLoopRound, valid: static r => r != null && r.Length > 0);
    }
}
