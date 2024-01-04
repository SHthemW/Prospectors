using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IBulletHitable
    {
        int HitTimesConsumption { get; }
        bool OverrideHitActions { get; }
        bool Enable { get; set; }

        void Hit(IBullet bullet, Vector3 position);
    }
}
