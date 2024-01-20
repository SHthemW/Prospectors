using Game.Interfaces;
using Game.Interfaces.Data;
using Game.Interfaces.GameObj;
using Game.Services.Combat;
using Game.Services.SAction;
using Game.Utils.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Combat
{
    internal sealed class WeaponCombatBehav : WeaponBehaviour
    {
        private ObjectSpawner<IBullet> _bulletShooter;

        private void Awake()
        {
            _bulletShooter = new();

            ThisWeapon.Magazine = new Magazine(
                maxCapacity: ThisWeapon.MaxMagazineCapacity,
                actAfterReload: static num => Debug.Log($"reload finished, get {num}."));
        }

        private void Start()
        {
            var gunActionImpl = new Dictionary<SActionDataTag, object>()
            {
                [SActionDataTag.PrimaryAnimator] = ThisWeapon.Animator,

                [SActionDataTag.GunShellSpawnInfo] = (
                    parent: ThisWeapon.ShellParent.Get(),
                    position: (Func<Vector3>)(() => ThisWeapon.ShellThrowingWindow.position),
                    rotation: (Func<Quaternion>)(() => ThisWeapon.ShellThrowingWindow.rotation),
                    pool: new ObjectSpawner<IDestoryManagedObject>()
                ),
                [SActionDataTag.GunFireSpawnInfo] = (
                    parent: ThisWeapon.Muzzle,
                    position: (Func<Vector3>)(() => ThisWeapon.Muzzle.position),
                    rotation: (Func<Quaternion>)(() => ThisWeapon.Muzzle.rotation),
                    pool: new ObjectSpawner<IDestoryManagedObject>()
                ),
            };

            var masterActionImpl = new Dictionary<SActionDataTag, object>()
            {
                [SActionDataTag.PrimaryAnimator] = ThisWeapon.MasterAnimators
            };

            foreach (var round in ThisWeapon.ShootingLoopRound)
            {
                IExecutableAction.BatchInit(gunActionImpl, round.GunActions);
                IExecutableAction.BatchInit(masterActionImpl, round.MasterActions);

                foreach (var unit in round)
                {
                    IExecutableAction.BatchInit(gunActionImpl, unit.GunActions);
                    IExecutableAction.BatchInit(masterActionImpl, unit.MasterActions);
                }
            }
        }

        private void Update()
        {
            UpdateShootingCd();

            if (ThisWeapon.TriggerIsPressing) 
                TryShootBullet();
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
                    bullet.Damage             = ThisWeapon.BulletDamage;
                    bullet.MaxExistingSeconds = ThisWeapon.BulletExistTimeSec;
                    bullet.Rigidbody.velocity = ThisWeapon.BulletStartSpeed * direction;
                    bullet.transform.position = ThisWeapon.Muzzle.position;
                    bullet.transform.SetParent(ThisWeapon.BulletParent.Get());

                    Array.ForEach(unit.GunActions, act => ((IExecutableAction)act).Execute());
                    Array.ForEach(unit.MasterActions, act => ((IExecutableAction)act).Execute());
                },  
                delayTimeSec: unit.ShootingDelaySecond, 
                coroutineMgr: this);
            }

            Array.ForEach(CurrentShootingRound.GunActions, act => ((IExecutableAction)act).Execute());
            Array.ForEach(CurrentShootingRound.MasterActions, act => ((IExecutableAction)act).Execute());

            ResetShootingCd();
        }
    }
}

