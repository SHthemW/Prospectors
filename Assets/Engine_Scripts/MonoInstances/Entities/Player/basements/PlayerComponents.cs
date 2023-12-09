using Game.Services.Combat;
using Game.Utils.Collections;
using UnityEngine;
using System;
using Game.Utils.Extensions;
using Game.Services.Animation;

namespace Game.Instances.Player
{
    internal sealed class PlayerComponents : MonoBehaviour
    {
        [Header("Data Define")]

        [SerializeField]
        private PlayerStaticData_SO _staticBasicData;
        internal PlayerStaticData_SO StaticBasicData 
            => _staticBasicData.AsSafeInspectorValue(name, o => o != null);

        [SerializeField]
        private AnimPropertyNameData_SO _animPropNames;
        internal AnimPropertyNameData_SO AnimPropNames
            => _animPropNames.AsSafeInspectorValue(name, o => o != null);

        [Header("Components Define")]

        [SerializeField]
        private Rigidbody _playerRb;
        internal Rigidbody PlayerRb 
            => _playerRb.AsSafeInspectorValue(name, rb => rb != null);

        [SerializeField]
        private Transform _rootTransform;
        internal Transform RootTransform 
            => _rootTransform.AsSafeInspectorValue(name, p => p != null);

        [SerializeField]
        private Transform _weaponParent;
        internal Transform WeaponParent 
            => _weaponParent.AsSafeInspectorValue(name, p => p != null);

        [SerializeField]
        private Animator[] _charAnimators;
        internal Animator[] CharAnimators
            => _charAnimators.AsSafeInspectorValue(name, p => p != null && p.Length > 0);

        [SerializeField]
        private TaggedItem<GameObject>[] _charModels;
        internal TaggedItem<GameObject>[] CharModels 
            => _charModels.AsSafeInspectorValue(name, p => p != null && p.Length > 0);

        [SerializeField]
        private TaggedItem<Transform>[] _charHands;
        internal TaggedItem<Transform>[] CharHands 
            => _charHands.AsSafeInspectorValue(name, p => p != null);

        [Header("External")]

        [SerializeField]
        private AimPoint _aimPoint;
        internal AimPoint AimPoint
            => _aimPoint.AsSafeInspectorValue(name, p => p != null);
    }
}