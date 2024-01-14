using Game.Interfaces;
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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Unhitable"))
                return;

            if (!collision.gameObject.CompareTag("Hitable"))
            {
                ThisBullet.CurrentHitTimes += OBSTACLE_TIMES_COST;
                Array.ForEach(ThisBullet.OnHitActions, a => ((IExecutableAction)a).ExecuteWith(_bulletActionImpl));
                return;
            }

            if (!collision.gameObject.TryGetComponent(out IBulletHitable hitable))
                throw new ArgumentException();

            hitable.Hit(ThisBullet, transform.position);
            ThisBullet.CurrentHitTimes += hitable.HitTimesConsumption;

            if (!hitable.OverrideHitActions)
                Array.ForEach(ThisBullet.OnHitActions, a => ((IExecutableAction)a).ExecuteWith(_bulletActionImpl));

            if (_enableHitDebug)
                Debug.Log("[bullet] hitted: " + collision.gameObject.name);
        }
    }
}
