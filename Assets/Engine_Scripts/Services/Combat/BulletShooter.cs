using Game.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Combat
{
    public sealed class BulletShooter
    {
        // TODO: require following component:

        // object pool service

        private readonly Transform _bulletParent;
        private readonly Transform _muzzle;

        public BulletShooter(Transform bulletParent, Transform muzzle)
        {
            _bulletParent  = bulletParent  != null ? bulletParent  : throw new ArgumentNullException(nameof(bulletParent));
            _muzzle = muzzle != null ? muzzle : throw new ArgumentNullException(nameof(muzzle));
            _bulletDataCache = new();
        }

        public void Shoot(in GameObject bulletObj, Vector3 direction, float speed, float existSecs)
        {
            if (bulletObj == null || direction == default || speed == 0) 
                throw new ArgumentException();

            GameObject bulletObjInstance = UnityEngine.Object.Instantiate
            (
                original: bulletObj,
                parent:   _bulletParent,
                position: _muzzle.position,
                rotation: Quaternion.identity
            );

            IBullet bullet = GetBulletDataOn(bulletObjInstance);

            bullet.Rigidbody.velocity = direction * speed;
            bullet.MaxExistingSeconds = existSecs;
        }

        private readonly Dictionary<GameObject, IBullet> _bulletDataCache;
        private IBullet GetBulletDataOn(in GameObject obj)
        {
            if (_bulletDataCache.TryGetValue(obj, out IBullet found))
            {
                return found;
            }
            else
            {
                if (obj.TryGetComponent<IBullet>(out var currentRb))
                {
                    _bulletDataCache.Add(obj, currentRb);
                    return currentRb;
                }
            }
            throw new Exception($"[comp] {nameof(IBullet)} component not found.");
        }
    }
}
