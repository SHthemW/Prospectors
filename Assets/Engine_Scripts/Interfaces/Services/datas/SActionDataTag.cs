using System;
using System.Collections.Generic;

namespace Game.Interfaces.Data
{
    [Flags]
    public enum SActionDataTag
    {
        // spawn gameobject
        HitEffectSpawnInfo = 1 << 0,
        HitHoleSpawnInfo   = 1 << 1,
        GunFireSpawnInfo   = 1 << 2,
        GunShellSpawnInfo  = 1 << 3,

        // play animation
        PrimaryAnimator    = 1 << 4,
    }

    public static class ScriptableActionTagExtensions
    {
        public static IEnumerable<SActionDataTag> Selections(this SActionDataTag value)
        {
            foreach (SActionDataTag flag in Enum.GetValues(typeof(SActionDataTag)))
                if (value.HasFlag(flag))
                    yield return flag;
        }
    }
}
