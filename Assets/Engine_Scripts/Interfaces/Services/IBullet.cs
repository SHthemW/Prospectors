using Game.Interfaces.GameObj;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IBullet : IDestoryManagedObject
    {
        Rigidbody Rigidbody { get; }

        float CurrentExistingSeconds { get; set; }
        float MaxExistingSeconds { get; set; }

        int MaxHitTimes { get; set; }
        int CurrentHitTimes { get; set; }

        int Damage { get; set; }
    }
}


