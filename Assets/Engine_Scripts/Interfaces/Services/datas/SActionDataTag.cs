using System;
using System.Collections.Generic;

namespace Game.Interfaces.Data
{
    public enum SActionDataTag : int
    {
        None               = 0,

        // spawn gameobject
        HitEffectSpawnInfo = 1,
        HitHoleSpawnInfo   = 2,
        GunFireSpawnInfo   = 3,
        GunShellSpawnInfo  = 4,

        // play animation
        PrimaryAnimator    = 5,
    }
}
