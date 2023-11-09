using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Combat
{
    internal sealed class RotatableWeapon : MonoBehaviour
    {
        [field: SerializeField]
        private Transform _followPoint;

        private void Update()
        {
            transform.forward = _followPoint.position - transform.position;
        }
    }
}
