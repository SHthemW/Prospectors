using Game.Interfaces;
using Game.Interfaces.GameObj;
using Game.Services.Combat;
using Game.Utils.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Combat
{
    internal sealed class WeaponCombatBehav : WeaponBehaviour
    {
        private ObjectSpawner<IBullet>               _bulletShooter;
        private ObjectSpawner<IDestoryManagedObject> _gunFireSpawner;
        private ObjectSpawner<IDestoryManagedObject> _bulletShellSpawner;

        private void Awake()
        {
            _bulletShooter      = new();
            _gunFireSpawner     = new();
            _bulletShellSpawner = new();

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
                    pool:   _bulletShellSpawner
                ),
                ["gunFireSpawnInfo"] = (
                    parent: ThisWeapon.Muzzle,
                    caster: ThisWeapon.Muzzle,
                    pool:   _gunFireSpawner
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
                Delay.Do(action: () =>
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
                    bullet.transform.position = ThisWeapon.Muzzle.position;
                    bullet.transform.SetParent(ThisWeapon.BulletParent.Get());

                    Array.ForEach(unit.GunActions, act => act.Implement(_gunActionImpl));
                    Array.ForEach(unit.MasterActions, act => act.Implement(_masterActionImpl));
                },  
                delayTimeSec: unit.ShootingDelaySecond, 
                coroutineMgr: this);
            }

            Array.ForEach(CurrentShootingRound.GunActions,    act => act.Implement(_gunActionImpl));
            Array.ForEach(CurrentShootingRound.MasterActions, act => act.Implement(_masterActionImpl));

            ResetShootingCd();
        }
    }
}

