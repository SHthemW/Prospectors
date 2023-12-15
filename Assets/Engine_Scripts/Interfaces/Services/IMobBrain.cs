using System;
using System.Collections.Generic;

namespace Game.Interfaces
{
    public interface IMobBrain
    {
        void Hit(IMob mob, int damage);
    }
}
