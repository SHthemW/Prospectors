using System.Collections;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IBullet
    {
        Rigidbody Rigidbody { get; }
        float MaxExistingSeconds { get; set; }

        int MaxHitTimes { get; set; }
        int CurrentHitTimes { get; set; }
    }
}


