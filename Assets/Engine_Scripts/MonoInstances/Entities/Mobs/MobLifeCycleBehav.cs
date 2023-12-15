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

        private void Update()
        {
            if (ThisMob.CurrentHealth <= 0)
                ThisMob.gameObject.SetActive(false);
        }
    }
}
