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
        private ShootingRound GetCurrentShootingRound()
        {
            if (_currentShootingRoundIndex >= ThisWeapon.ShootingLoopRound.Length)
                _currentShootingRoundIndex = 0;

            var currentRound = ThisWeapon.ShootingLoopRound[_currentShootingRoundIndex];
            _currentShootingRoundIndex++;

            return currentRound;
        }

        // weapon action

        private void TryShootBullet()
        {
            if (InShootingCd)
                return;

            var currentRound = GetCurrentShootingRound();

            _bulletShooter.Shoot(
                bulletObj: currentRound.Bullet, 
                direction: ThisWeapon.AimingDirection, 
                speed:     ThisWeapon.BulletStartSpeed);
            ResetShootingCd();
        }

    }
}

