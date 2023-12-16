﻿using Game.Interfaces;
using Game.Services.Animation;
using Game.Services.Physics;
using Game.Utils.Attributes;
using Game.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobDataAndComponentHandler : MonoBehaviour, IMob, IDataHolder<IAnimationStateName>
    {
        // static datas

        [SerializeField]
        private MobStaticData_SO _staticData;

        [SerializeField]
        private MobBrain_SO _brain;
        internal IMobCombatBrain Brain 
            => _brain.AsSafeInspectorValue(name, b => b != null);

        [SerializeField]
        private AnimPropertyNameData_SO _animPropertyName;
        IAnimationStateName IDataHolder<IAnimationStateName>.Data 
            => _animPropertyName.AsSafeInspectorValue(name, a => a != null);

        internal int HitTimesConsumption => _staticData.HitTimesConsumption;
        internal bool OverrideHitActions => _staticData.OverrideHitActions;
        internal ExecutableAction[] OnHittedActions => _staticData.OnHittedActions;

        // components ref
        [Header("Components")]

        [SerializeField]
        private Animator _animator;

        public SingletonComponent<Transform> HitEffectParent { get; set; } = new("@HitEffects");
        public SingletonComponent<Transform> HitHoleParent { get; set; } = new("@HitHoles");

        // implements

        [field: Header("Datas")]

        [field: SerializeField, Utils.Attributes.ReadOnly]
        public int CurrentHealth { get; set; }

        int IMob.MaxHealth => _staticData.MaxHealth;
        Animator IMob.Animator 
            => _animator.AsSafeInspectorValue(name, a => a != null);
        IAnimationStateName IMob.AnimNames 
            => _animPropertyName.AsSafeInspectorValue(name, a => a != null);
    }
}
