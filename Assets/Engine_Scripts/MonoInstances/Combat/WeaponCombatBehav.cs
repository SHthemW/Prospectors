using Game.Services.Combat;
using Game.Services.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Combat
{
    internal sealed class WeaponCombatBehav : WeaponBehaviour
    {
        private BulletShooter _bulletShooter;

        private void Awake()
        {
            _bulletShooter = new(
                bulletParent: ThisWeapon.BulletParent.Get(), 
                muzzle:       ThisWeapon.Muzzle);
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

        // weapon action

        private void TryShootBullet()
        {
            if (InShootingCd)
                return;

            foreach (ShootingUnit unit in CurrentShootingRound)
            {
                _bulletShooter.Shoot(
                bulletObj: unit.Bullet,
                direction: ThisWeapon.AimingDirection,
                existSecs: ThisWeapon.BulletExistTimeSec,
                speed: ThisWeapon.BulletStartSpeed
                );
            }

            ResetShootingCd();
        }

    }
}

