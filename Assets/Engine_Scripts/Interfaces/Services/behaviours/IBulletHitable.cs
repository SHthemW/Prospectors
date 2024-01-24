using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IBulletHitable
    {
        int HitTimesConsumption { get; }
        void Hit(IBullet bullet, Vector3 position);
    }
}
