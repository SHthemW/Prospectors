using Game.Interfaces;
using System;
using UnityEngine;

namespace Game.Instances.Combat
{
    [RequireComponent(typeof(IBullet))]
    internal sealed class BulletCollisionBehav : BulletBehaviour
    {
        private const int OBSTACLE_TIMES_COST = 99;

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Hitable"))
            {
                ThisBullet.CurrentHitTimes += OBSTACLE_TIMES_COST;
                return;
            }

            if (!collision.gameObject.TryGetComponent(out IBulletHitable hitable))
                throw new ArgumentException();

            hitable.Hit(ThisBullet);
            ThisBullet.CurrentHitTimes += hitable.HitTimesConsumption;
        }
    }
}
