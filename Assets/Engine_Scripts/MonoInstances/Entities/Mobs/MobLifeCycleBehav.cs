using System;
using System.Collections.Generic;

namespace Game.Instances.Mob
{
    internal sealed class MobLifeCycleBehav : MobBehaviour
    {
        private void Start()
        {
            ThisMob.Brain.Init(ThisMob);
        }
    }
}
