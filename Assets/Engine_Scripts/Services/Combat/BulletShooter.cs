using Game.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Combat
{
    public sealed class BulletShooter
    {
        // TODO: require following component:

        // object pool service
        private readonly Transform     _muzzle;
        private readonly Transform     _bulletParent;
        private readonly MonoBehaviour _coroutineMaster;

        public BulletShooter(Transform bulletParent, Transform muzzle, MonoBehaviour coroutineMaster)
        {
            _muzzle = muzzle != null ? muzzle : throw new ArgumentNullException(nameof(muzzle));
            _bulletParent = bulletParent != null ? bulletParent : throw new ArgumentNullException(nameof(bulletParent));
            _coroutineMaster = coroutineMaster != null ? coroutineMaster : throw new ArgumentNullException(nameof(coroutineMaster));
            _bulletDataCache = new();
        }

        public void ShootImmediately(in GameObject bulletObj, Vector3 direction, float speed, float existSecs)
        {
            if (bulletObj == null || direction == default || speed == 0) 
                throw new ArgumentException();

            if (direction != direction.normalized)
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
        public void Shoot(GameObject bulletObj, Vector3 direction, float speed, float existSecs, float delaySec)
        {
            if (delaySec == 0)
            {
                ShootImmediately(bulletObj, direction, speed, existSecs);
                return;
            }
            else
            {
                _coroutineMaster.StartCoroutine(DelayShootingCoroutine());
                return;
            }

            IEnumerator DelayShootingCoroutine()
            {
                yield return new WaitForSeconds(delaySec);
                ShootImmediately(bulletObj, direction, speed, existSecs);
            }
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
