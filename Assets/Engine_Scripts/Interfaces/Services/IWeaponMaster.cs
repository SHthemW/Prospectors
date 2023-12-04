using Game.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IWeaponMaster
    {
        // properties
        IWeapon CurrentWeapon { get; set; }
        Vector3 AimingPosition { get; }
        Vector3 CenterPosition { get; }
        Func<Vector3> CurrentHandPositionGetter { get; }
        Animator[] CharacterAnimators { get; }

        // status
        bool WantToShoot { get; }

        /// <summary>
        /// when magzine run out, call this function to get bullet from inventory.
        /// </summary>
        /// <param name="require"></param>
        /// <returns>actuall get (sometimes may not enough)</returns>
        int TryGetBulletFromInventory(int require);
    }
}
