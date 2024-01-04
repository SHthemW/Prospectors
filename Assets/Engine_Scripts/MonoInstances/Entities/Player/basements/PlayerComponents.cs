using Game.Services.Combat;
using Game.Utils.Collections;
using UnityEngine;
using System;
using Game.Utils.Extensions;
using Game.Services.Animation;
using Game.Interfaces;

namespace Game.Instances.Player
{
    internal sealed class PlayerComponents : MonoBehaviour
    {
        private readonly static Checker safe = new(nameof(PlayerComponents));

        [Header("Data Define")]

        [SerializeField]
        private PlayerStaticData_SO _staticBasicData;
        internal PlayerStaticData_SO StaticBasicData 
            => safe.Checked(_staticBasicData);

        [SerializeField]
        private AnimPropertyNameData_SO _animPropNames;
        internal AnimPropertyNameData_SO AnimPropNames
            => safe.Checked(_animPropNames);

        [Header("Components Define")]

        [SerializeField]
        private Rigidbody _playerRb;
        internal Rigidbody PlayerRb 
            => safe.Checked(_playerRb);

        [SerializeField]
        private Transform _rootTransform;
        internal Transform RootTransform 
            => safe.Checked(_rootTransform);

        [SerializeField]
        private Transform _weaponParent;
        internal Transform WeaponParent 
            => safe.Checked(_weaponParent);

        [SerializeField]
        private Animator[] _charAnimators;
        internal Animator[] CharAnimators
            => safe.Checked(_charAnimators, static p => p != null && p.Length > 0);

        [SerializeField]
        private TaggedItem<GameObject>[] _charModels;
        internal TaggedItem<GameObject>[] CharModels 
            => safe.Checked(_charModels, static p => p != null && p.Length > 0);

        [SerializeField]
        private TaggedItem<Transform>[] _charHands;
        internal TaggedItem<Transform>[] CharHands 
            => safe.Checked(_charHands);

        [Header("External")]

        [SerializeField]
        private AimPoint _aimPoint;
        internal AimPoint AimPoint
            => safe.Checked(_aimPoint);
    }
}