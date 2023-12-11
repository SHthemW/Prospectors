using Game.Interfaces.GameObj;
using Game.Services.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Combat
{
    internal sealed class BulletExistBehav : BulletBehaviour
    {
        private Dictionary<string, object>           _bulletActionImpl;
        private ObjectSpawner<IDestoryManagedObject> _hitEffectSpawner;

        private void Awake()
        {
            _hitEffectSpawner = new();    
        }
        private void Start()
        {
            _bulletActionImpl = new()
            {
                ["hitEffectSpawnInfo"] = (
                parent: ThisBullet.HitEffectParent.Get(),
                caster: transform,
                pool: _hitEffectSpawner
                )
            };
        }

        private void OnEnable()
        {
            ThisBullet.CurrentHitTimes = 0;
            ThisBullet.CurrentExistingSeconds = 0;
        }

        private void Update()
        {
            ThisBullet.CurrentExistingSeconds += Time.deltaTime;

            if (ThisBullet.CurrentExistingSeconds >= ThisBullet.MaxExistingSeconds
            ||  ThisBullet.CurrentHitTimes        >= ThisBullet.MaxHitTimes)
            {
                foreach (var action in ThisBullet.OnBulletDisableActions)
                    action.Implement(_bulletActionImpl);

                ThisBullet.DeactiveAction?.Invoke(gameObject); 
            }
        }
    }
}
