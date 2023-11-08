using Game.Services.Combat;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerComponents : MonoBehaviour
    {       
        [field: SerializeField]
        internal Rigidbody PlayerRb { get; private set; }

        [field: SerializeField]
        internal Animator CharAnimator { get; private set; }

        [field: SerializeField]
        internal Transform RootTransform { get; private set; }

        [field: SerializeField]
        internal AimPoint AimPoint { get; private set; }

        [field: SerializeField]
        internal Transform AimBone { get; private set; }
    }
}