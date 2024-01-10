﻿using Game.Interfaces;
using Game.Interfaces.GameObj;
using Game.Services.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobHitableBehav : MobBehaviour, IBulletHitable, IEnableOnAliveOnly
    {
        private Dictionary<string, object>           _mobActionImpl;
        private ObjectSpawner<IDestoryManagedObject> _hitEffectSpawner;
        private ObjectSpawner<IDestoryManagedObject> _hitHoleSpawner;

        private void Awake()
        {
            _hitEffectSpawner = new();
            _hitHoleSpawner = new();
        }

        private Vector3 _currentHittedPosition;

        private void Start()
        {
            _mobActionImpl = new()
            {
                ["hitEffectSpawnInfo"] = (
                parent:   ThisMob.HitEffectParent.Get(),
                position: (Func<Vector3>)   (() => _currentHittedPosition),
                rotation: (Func<Quaternion>)(() => transform.rotation),
                pool:     _hitEffectSpawner
                ),
                ["hitHoleSpawnInfo"] = (
                parent:   ThisMob.HitHoleParent.Get(), 
                position: (Func<Vector3>)   (() => _currentHittedPosition),
                rotation: (Func<Quaternion>)(() => transform.rotation),
                pool:     _hitHoleSpawner
                )
            };
        }

        public bool Enable { get; set; } = true;

        int IBulletHitable.HitTimesConsumption => ThisMob.HitTimesConsumption;
        bool IBulletHitable.OverrideHitActions => ThisMob.OverrideHitActions;
        void IBulletHitable.Hit(IBullet bullet, Vector3 position)
        {
            if (!this.Enable)
                return;

            _currentHittedPosition = position;

            foreach (var action in ThisMob.OnHittedActions)
                action.Implement(_mobActionImpl);

            CombatUtil.Hit(
                who:    ThisMob.Health.Get(),
                damage: bullet.Damage,
                anim:   (animator: ThisMob.Animator, name: ThisMob.AnimStateNames));
        }
    }
}