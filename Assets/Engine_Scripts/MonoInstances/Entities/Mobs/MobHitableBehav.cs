using Game.Interfaces;
using Game.Interfaces.Data;
using Game.Interfaces.GameObj;
using Game.Services.Combat;
using Game.Services.SAction;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobHitableBehav : MobBehaviour, IBulletHitable, IEnableOnAliveOnly
    {
        private Vector3 _currentHittedPosition;

        private void Start()
        {
            IExecutableAction.BatchInit(kwargs: new()
            {
                [SActionDataTag.HitEffectSpawnInfo] = (
                parent:   ThisMob.HitEffectParent.Get(),
                position: (Func<Vector3>)   (() => _currentHittedPosition),
                rotation: (Func<Quaternion>)(() => transform.rotation),
                pool:     new ObjectSpawner<IDestoryManagedObject>()
                ),
                [SActionDataTag.HitHoleSpawnInfo] = (
                parent:   ThisMob.HitHoleParent.Get(),
                position: (Func<Vector3>)   (() => _currentHittedPosition),
                rotation: (Func<Quaternion>)(() => transform.rotation),
                pool:     new ObjectSpawner<IDestoryManagedObject>()
                )
            },
            ThisMob.OnHittedActions,
            ThisMob.OnDeadActions);
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
                ((IExecutableAction)action).Execute();

            CombatUtil.Hit(
                who:    ThisMob.Health.Get(),
                damage: bullet.Damage,
                anim:   (animator: ThisMob.Animator, name: ThisMob.AnimStateNames));
        }
    }
}
