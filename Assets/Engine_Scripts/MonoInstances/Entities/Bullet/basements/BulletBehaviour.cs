using Game.Interfaces;
using Game.Interfaces.GameObj;
using Game.Services.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Combat
{
    [RequireComponent(typeof(BulletDataAndComponentHandler))]
    internal abstract class BulletBehaviour : MonoBehaviour
    {
        protected Dictionary<ScriptableActionTag, object> _bulletActionImpl;

        private void Awake()
        {
            _bulletActionImpl = new()
            {
                [ScriptableActionTag.HitEffectSpawnInfo] = (
                parent:   ThisBullet.HitEffectParent.Get(),
                position: (Func<Vector3>)   (() => transform.position),
                rotation: (Func<Quaternion>)(() => transform.rotation),
                pool:     new ObjectSpawner<IDestoryManagedObject>()
                ),
                [ScriptableActionTag.HitHoleSpawnInfo] = (
                parent:   ThisBullet.HitHoleParent.Get(),
                position: (Func<Vector3>)   (() => transform.position),
                rotation: (Func<Quaternion>)(() => transform.rotation),
                pool:     new ObjectSpawner<IDestoryManagedObject>()
                )
            };
        }

        private BulletDataAndComponentHandler _thisBullet;
        protected BulletDataAndComponentHandler ThisBullet 
        { 
            get 
            { 
                if (_thisBullet == null)
                    _thisBullet = GetComponent<BulletDataAndComponentHandler>();
                return _thisBullet; 
            } 
        }
    }
}
