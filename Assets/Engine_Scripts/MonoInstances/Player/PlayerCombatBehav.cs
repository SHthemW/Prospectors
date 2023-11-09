using Game.Interfaces.GameObj;
using Game.Services.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerCombatBehav : PlayerBehaviour, IWeaponAimMaster
    {
        private WeaponAimTurnUpdater _aimTurnUpdater;

        Vector3 IWeaponAimMaster.AimingPosition => Components.AimPoint.transform.position;

        private void Awake()
        {
            Components.AimPoint.Init(Components.RootTransform, DataHandler.MaxFollowOffsetDuringAim);

            _aimTurnUpdater = new(Components.AimPoint, Components.AimBone, DataHandler.AimHeight);
        }
        private void Update()
        {           
            _aimTurnUpdater.UpdateAimTurn();
        }
    }
}
