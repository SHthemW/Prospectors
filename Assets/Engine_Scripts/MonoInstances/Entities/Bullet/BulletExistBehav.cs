using System;
using System.Collections.Generic;

namespace Game.Instances.Combat
{
    internal sealed class BulletExistBehav : BulletBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, ThisBullet.MaxExistingSeconds);
        }

        private void Update()
        {
            if (ThisBullet.CurrentHitTimes >= ThisBullet.MaxHitTimes)
                Destroy(gameObject);
        }
    }
}
