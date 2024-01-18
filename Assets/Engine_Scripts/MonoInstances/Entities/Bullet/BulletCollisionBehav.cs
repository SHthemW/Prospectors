using Game.Interfaces;
using Game.Interfaces.Data;
using Game.Interfaces.GameObj;
using Game.Services.Combat;
using System;
using UnityEngine;

namespace Game.Instances.Combat
{
    [RequireComponent(typeof(IBullet))]
    internal sealed class BulletCollisionBehav : BulletBehaviour
    {
        [SerializeField]
        private bool _enableHitDebug;

        private const int OBSTACLE_TIMES_COST = 99;

        private void Start()
        {
            IExecutableAction.BatchInit(new()
            {
                [SActionDataTag.HitEffectSpawnInfo] = (
                parent:   ThisBullet.HitEffectParent.Get(),
                position: (Func<Vector3>)(() => transform.position),
                rotation: (Func<Quaternion>)(() => transform.rotation),
                pool:     new ObjectSpawner<IDestoryManagedObject>()
                ),
                [SActionDataTag.HitHoleSpawnInfo] = (
                parent:   ThisBullet.HitHoleParent.Get(),
                position: (Func<Vector3>)(() => transform.position),
                rotation: (Func<Quaternion>)(() => transform.rotation),
                pool:     new ObjectSpawner<IDestoryManagedObject>()
                )
            },
            ThisBullet.OnHitActions);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Unhitable"))
                return;

            if (!collision.gameObject.CompareTag("Hitable"))
            {
                ThisBullet.CurrentHitTimes += OBSTACLE_TIMES_COST;
                Array.ForEach(ThisBullet.OnHitActions, a => ((IExecutableAction)a).Execute());
                return;
            }

            if (!collision.gameObject.TryGetComponent(out IBulletHitable hitable))
                throw new ArgumentException();

            hitable.Hit(ThisBullet, transform.position);
            ThisBullet.CurrentHitTimes += hitable.HitTimesConsumption;

            if (!hitable.OverrideHitActions)
                Array.ForEach(ThisBullet.OnHitActions, a => ((IExecutableAction)a).Execute());

            if (_enableHitDebug)
                Debug.Log("[bullet] hitted: " + collision.gameObject.name);
        }
    }
}
