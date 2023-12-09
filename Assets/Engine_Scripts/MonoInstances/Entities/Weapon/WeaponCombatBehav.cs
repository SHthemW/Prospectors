using Game.Interfaces;
using Game.Services.Combat;
using Game.Utils.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Instances.Combat
{
    internal sealed class WeaponCombatBehav : WeaponBehaviour
    {
        private ObjectSpawner<IBullet> _bulletShooter;

        private ObjectPool<GameObject> _shellPool;
        private ObjectPool<GameObject> _gunFirePool;

        private void Awake()
        {
            _bulletShooter = new();

            ThisWeapon.Magazine = new Magazine(
                maxCapacity: ThisWeapon.MaxMagazineCapacity,
                actAfterReload: static num => Debug.Log($"reload finished, get {num}."));
        }

        private Dictionary<string, object> _gunActionImpl;
        private Dictionary<string, object> _masterActionImpl;

        private void Start()
        {
            _gunActionImpl = new()
            {
                ["anim1"] = ThisWeapon.Animator,

                ["shellSpawnInfo"] = (
                    parent: ThisWeapon.ShellParent.Get(),
                    caster: ThisWeapon.ShellThrowingWindow,
                    poolGt: (Func<ObjectPool<GameObject>>) (() => _shellPool),
                    poolSt: (Action<ObjectPool<GameObject>>)(p => _shellPool = p)
                ),
                ["gunFireSpawnInfo"] = (
                    parent: ThisWeapon.Muzzle,
                    caster: ThisWeapon.Muzzle,
                    poolGt: (Func<ObjectPool<GameObject>>) (() => _gunFirePool),
                    poolSt: (Action<ObjectPool<GameObject>>)(p => _gunFirePool = p)
                ),
            };
            _masterActionImpl = new()
            {
                ["anim1"] = ThisWeapon.MasterAnimators
            };
        }

        private void Update()
        {
            UpdateShootingCd();

            if (ThisWeapon.TriggerIsPressing) 
            {
                TryShootBullet(); // TODO: CD
            }
        }

        // shooting cd

        private float _currentShootingCd = 0;
        private bool InShootingCd 
            => _currentShootingCd > 0;
        private void ResetShootingCd()
        {
            _currentShootingCd = ThisWeapon.ShootingCdSec;
        }
        private void UpdateShootingCd()
        {
            if (_currentShootingCd > 0)
                _currentShootingCd -= Time.deltaTime;
        }

        // shooting round

        private int _currentShootingRoundIndex = 0;
        private ShootingRound CurrentShootingRound
            => ShootingRound.GetCurrentRound
            (
                ref _currentShootingRoundIndex,
                ThisWeapon.ShootingLoopRound
            );

        // reload & inventory
        private void TryReloadBullet()
        {
            Debug.Log("magazine is run out.");
            ThisWeapon.Magazine.Reload(reloadNum: ThisWeapon.MaxBulletNumberFromMaster);
        }

        // weapon action
        private void TryShootBullet()
        {
            if (InShootingCd)
                return;

            if (!ThisWeapon.Magazine.TryUse())
            {
                TryReloadBullet();
                return;
            }

            foreach (ShootingUnit unit in CurrentShootingRound)
            {
                IBullet bullet = _bulletShooter.Spawn(unit.Bullet);

                var direction = ThisWeapon
                    .AimingDirection
                    .RotateAloneAxisY(clockwiseAngle: unit.ShootingAngleOffset)
                    .RotateAloneAxisY(clockwiseAngle: ThisWeapon.BulletAccuracyOffsetAngle)
                    .normalized;

                // init bullet
                bullet.MaxExistingSeconds = ThisWeapon.BulletExistTimeSec;
                bullet.Rigidbody.velocity = ThisWeapon.BulletStartSpeed * direction;
                bullet.Transform.position = ThisWeapon.Muzzle.position;
                bullet.Transform.SetParent( ThisWeapon.BulletParent.Get());

                Array.ForEach(unit.GunActions,    act => act.Implement(_gunActionImpl));
                Array.ForEach(unit.MasterActions, act => act.Implement(_masterActionImpl));
            }

            Array.ForEach(CurrentShootingRound.GunActions,    act => act.Implement(_gunActionImpl));
            Array.ForEach(CurrentShootingRound.MasterActions, act => act.Implement(_masterActionImpl));

            ResetShootingCd();
        }
    }
}

