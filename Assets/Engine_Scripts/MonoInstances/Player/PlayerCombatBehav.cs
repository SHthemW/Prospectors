using Game.Interfaces.GameObj;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerCombatBehav : PlayerBehaviour, IWeaponAimMaster
    {
        Vector3 IWeaponAimMaster.AimingPosition => Components.AimPoint.transform.position;
    }
}
