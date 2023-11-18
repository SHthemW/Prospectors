using Game.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IWeaponMaster
    {
        IWeapon CurrentWeapon { get; set; }
        Vector3 AimingPosition { get; }
        Func<Vector3> CurrentHandPositionGetter { get; }
    }
}
