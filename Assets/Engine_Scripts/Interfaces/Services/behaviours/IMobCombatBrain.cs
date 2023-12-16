using System;
using System.Collections.Generic;

namespace Game.Interfaces
{
    public interface IMobCombatBrain
    {
        void Init(IMob mob);
        void Hit(IMob mob, int damage);
        void Die(IMob mob);
    }
}
