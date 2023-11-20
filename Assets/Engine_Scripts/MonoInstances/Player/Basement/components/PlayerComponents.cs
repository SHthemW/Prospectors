using Game.Services.Combat;
using Game.Utils;
using UnityEngine;
using System;
using static Game.Utils.InspectorPropertyUtil;

namespace Game.Instances.Player
{
    internal sealed class PlayerComponents : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _playerRb;
        internal Rigidbody PlayerRb 
            => _playerRb.TryGetIf(name, rb => rb != null);

        [SerializeField]
        private Animator[] _charAnimators;
        internal Animator[] CharAnimators 
            => _charAnimators.TryGetIf(name, p => p != null && p.Length > 0);

        [SerializeField]
        private Transform _rootTransform;
        internal Transform RootTransform 
            => _rootTransform.TryGetIf(name, p => p != null);

        [SerializeField]
        private Transform _weaponParent;
        internal Transform WeaponParent 
            => _weaponParent.TryGetIf(name, p => p != null);

        [SerializeField]
        private TaggedItem<GameObject>[] _charModels;
        internal TaggedItem<GameObject>[] CharModels 
            => _charModels.TryGetIf(name, p => p != null && p.Length > 0);

        [SerializeField]
        private TaggedItem<Transform>[] _charHands;
        internal TaggedItem<Transform>[] CharHands 
            => _charHands.TryGetIf(name, p => p != null);

        [Header("External")]

        [SerializeField]
        private AimPoint _aimPoint;
        internal AimPoint AimPoint
            => _aimPoint.TryGetIf(name, p => p != null);

        [SerializeField]
        private Transform _aimBone;
        internal Transform AimBone
            => _aimBone.TryGetIf(name, p => p != null);
    }
}