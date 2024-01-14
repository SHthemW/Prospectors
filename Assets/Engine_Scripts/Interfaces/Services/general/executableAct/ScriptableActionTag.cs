using System;
using System.Collections.Generic;

namespace Game.Interfaces
{
    [Flags]
    public enum ScriptableActionTag
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
        public static IEnumerable<ScriptableActionTag> Selections(this ScriptableActionTag value)
        {
            foreach (ScriptableActionTag flag in Enum.GetValues(typeof(ScriptableActionTag)))
                if (value.HasFlag(flag))
                    yield return flag;
        }
    }
}
