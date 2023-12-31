﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Combat
{
    internal sealed class WeaponTransformBehav : WeaponBehaviour
    {
        private void Update()
        {
            FollowTheAim();
            SyncToMasterHand();
        }

        private void FollowTheAim()
        {
            var lookatPoint = ThisWeapon.AimingPosition - ThisWeapon.MasterPosition;
            transform.forward = new Vector3
            {
                x = lookatPoint.x,
                y = 0,
                z = lookatPoint.z
            };
        }

        private void SyncToMasterHand()
        {
            // sync shake
            transform.position = new Vector3
            {
                x = ThisWeapon.HandlePosition.x,
                y = ThisWeapon.HandlePosition.y,
                z = ThisWeapon.HandlePosition.z
            };
        }
    }
}
