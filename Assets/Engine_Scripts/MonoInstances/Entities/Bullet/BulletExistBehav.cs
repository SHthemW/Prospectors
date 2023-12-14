using Game.Interfaces.GameObj;
using Game.Services.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Combat
{
    internal sealed class BulletExistBehav : BulletBehaviour
    {
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
                ThisBullet.DeactiveAction?.Invoke(gameObject); 
            }
        }
    }
}
