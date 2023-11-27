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
            _bulletRbCache = new();
        }

        public void Shoot(in GameObject bulletObj, Vector3 direction, float speed)
        {
            if (bulletObj == null || direction == default || speed == 0) 
                throw new ArgumentException();

            var bullet   = UnityEngine.Object.Instantiate(
                original: bulletObj,
                parent:   _bulletParent,
                position: _muzzle.position,
                rotation: Quaternion.identity
                );
            var bulletRb = GetRigidbodyOn(bullet);

            bulletRb.velocity = direction * speed;
        }

        private readonly Dictionary<GameObject, Rigidbody> _bulletRbCache;
        private Rigidbody GetRigidbodyOn(in GameObject obj)
        {
            if (_bulletRbCache.TryGetValue(obj, out Rigidbody found))
            {
                return found;
            }
            else
            {
                if (obj.TryGetComponent<Rigidbody>(out var currentRb))
                {
                    _bulletRbCache.Add(obj, currentRb);
                    return currentRb;
                }
            }
            throw new Exception("[comp] rb component not found.");
        }
    }
}
