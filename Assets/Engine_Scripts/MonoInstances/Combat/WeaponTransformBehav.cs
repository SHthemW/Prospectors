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
        }

        private void FollowTheAim()
        {
            transform.forward = ThisWeapon.AimingPosition - transform.position;
        }
    }
}
