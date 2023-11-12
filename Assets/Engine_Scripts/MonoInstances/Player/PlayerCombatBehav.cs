using Game.Interfaces.GameObj;
using Game.Services.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerCombatBehav : PlayerBehaviour, IWeaponAimMaster
    {
        Vector3 IWeaponAimMaster.AimingPosition => Components.AimPoint.transform.position;

        private void Awake()
        {
            Components.AimPoint.Init(Components.RootTransform, DataHandler.MaxFollowOffsetDuringAim);
        }
    }
}
