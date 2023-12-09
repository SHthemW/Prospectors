using Game.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Services.Combat
{
    public sealed class BulletShooter
    {
        private readonly Transform     _muzzle;
        private readonly Transform     _bulletParent;
        private readonly MonoBehaviour _coroutineMaster;

        private readonly ObjectPool<GameObject>          _bulletObjPool;
        private readonly Dictionary<GameObject, IBullet> _bulletDataPool;

        private GameObject _currentBullet;

        public BulletShooter(Transform bulletParent, Transform muzzle, MonoBehaviour coroutineMaster)
        {
            _muzzle = muzzle != null ? muzzle : throw new ArgumentNullException(nameof(muzzle));
            _bulletParent = bulletParent != null ? bulletParent : throw new ArgumentNullException(nameof(bulletParent));
            _coroutineMaster = coroutineMaster != null ? coroutineMaster : throw new ArgumentNullException(nameof(coroutineMaster));

            _bulletDataPool = new();
            _bulletObjPool  = new(
                createFunc:      () => UnityEngine.Object.Instantiate(_currentBullet, _bulletParent),
                actionOnGet:     go => go.SetActive(true),
                actionOnRelease: go => go.SetActive(false),
                actionOnDestroy: go => UnityEngine.Object.Destroy(go)
            );
        }

        public void ShootImmediately(in GameObject bulletObj, Vector3 direction, float speed, float existSecs)
        {
            if (bulletObj == null || direction == default || speed == 0) 
                throw new ArgumentException();

            if (direction != direction.normalized)
                throw new ArgumentException();

            _currentBullet = bulletObj;
            GameObject bulletObjInstance = _bulletObjPool.Get();

            IBullet bullet = GetBulletDataOnPool(key: bulletObjInstance);

            bullet.DeactiveAction     = _bulletObjPool.Release;
            bullet.Transform.position = _muzzle.position;
            bullet.Rigidbody.velocity = direction * speed;
            bullet.MaxExistingSeconds = existSecs;
        }
        public void Shoot(GameObject bulletObj, Vector3 direction, float speed, float existSecs, float delaySec)
        {
            if (delaySec == 0)
                ShootImmediately(bulletObj, direction, speed, existSecs);
            else
                _coroutineMaster.StartCoroutine(DelayShootingCoroutine());

            IEnumerator DelayShootingCoroutine()
            {
                yield return new WaitForSeconds(delaySec);
                ShootImmediately(bulletObj, direction, speed, existSecs);
            }
        }

        private BulletShooter()
            => throw new NotImplementedException();
        private IBullet GetBulletDataOnPool(in GameObject key)
        {
            if (_bulletDataPool.TryGetValue(key, out IBullet found))
            {
                return found;
            }
            else
            {
                if (key.TryGetComponent<IBullet>(out var bullet))
                {
                    _bulletDataPool.Add(key, bullet);
                    return bullet;
                }
            }
            throw new Exception($"[comp] {nameof(IBullet)} component not found.");
        }
    }
}
