using System;
using System.Collections.Generic;

namespace Game.Interfaces
{
    public interface IBulletHitable
    {
        int HitTimesConsumption { get; }

        void Hit(IBullet bullet);
    }
}
