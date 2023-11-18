using System;
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
            var lookatPoint = ThisWeapon.AimingPosition - transform.position;
            transform.forward = new Vector3
            {
                x = lookatPoint.x,
                y = 0,
                z = lookatPoint.z
            };
        }

        private void SyncToMasterHand()
        {
            transform.position = new Vector3
            {
                x = transform.position.x,
                y = ThisWeapon.HandlePosition.y,
                z = transform.position.z
            };
        }
    }
}
