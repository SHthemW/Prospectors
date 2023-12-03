using Game.Services.Combat;
using Game.Services.Physics;
using Game.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

namespace Game.Instances.Combat
{
    internal sealed class WeaponCombatBehav : WeaponBehaviour
    {
        private BulletShooter _bulletShooter;

        private void Awake()
        {
            _bulletShooter = new(
                bulletParent: ThisWeapon.BulletParent.Get(), 
                muzzle:       ThisWeapon.Muzzle,
                coroutineMaster: this);

            ThisWeapon.Magazine = new Magazine(
                maxCapacity: ThisWeapon.MaxMagazineCapacity,
                actAfterReload: static num => Debug.Log($"reload finished, get {num}.")
                );
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
                _bulletShooter.Shoot(
                bulletObj: 
                    unit.Bullet,
                direction: 
                    ThisWeapon
                    .AimingDirection
                    .RotateAloneAxisY(clockwiseAngle: unit.ShootingAngleOffset)
                    .RotateAloneAxisY(clockwiseAngle: ThisWeapon.BulletAccuracyOffsetAngle)
                    .normalized,
                delaySec:
                    unit.ShootingDelaySecond,
                existSecs: 
                    ThisWeapon.BulletExistTimeSec,
                speed: 
                    ThisWeapon.BulletStartSpeed
                );

                if (unit.UnitAction != null)
                    unit.UnitAction.Execute(null);
            }

            if (CurrentShootingRound.RoundAction != null)
                CurrentShootingRound.RoundAction.Execute(null);

            ResetShootingCd();
        }
    }
}

