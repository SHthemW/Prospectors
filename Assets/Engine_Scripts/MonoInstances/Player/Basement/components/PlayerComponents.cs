using Game.Services.Combat;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerComponents : MonoBehaviour
    {      
        [field: SerializeField]
        internal Rigidbody PlayerRb { get; private set; }

        [field: SerializeField]
        internal Animator[] CharAnimator { get; private set; }

        [field: SerializeField]
        internal Transform RootTransform { get; private set; }

        [field: SerializeField]
        internal Transform WeaponParent { get; private set; }

        [field: SerializeField]
        internal GameObject[] PlayerModels { get; private set; }
        internal readonly int[] FrontModelIndexList = { 0 };
        internal readonly int[] BackModelIndexList  = { 1 };

        [field: Header("External")]
        [field: SerializeField]
        internal AimPoint AimPoint { get; private set; }

        [field: SerializeField]
        internal Transform AimBone { get; private set; }
    }
}