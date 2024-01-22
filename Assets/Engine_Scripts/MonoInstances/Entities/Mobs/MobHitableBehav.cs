﻿using Game.Interfaces;
using Game.Interfaces.Data;
using Game.Interfaces.GameObj;
using Game.Services.Combat;
using Game.Services.SAction;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobHitableBehav : MobBehaviour, IBulletHitable, IEnableOnAliveOnly, IHoldCharHitPosition
    {
        public Vector3 CurrentHittedPosition { get; set; }
        public bool Enable { get; set; } = true;

        int IBulletHitable.HitTimesConsumption => ThisMob.HitTimesConsumption;
        bool IBulletHitable.OverrideHitActions => ThisMob.OverrideHitActions;
        void IBulletHitable.Hit(IBullet bullet, Vector3 position)
        {
            if (!this.Enable)
                return;

            CurrentHittedPosition = position;

            var health = ThisMob.Health.Get();
            health.CurrentHealth -= bullet.Damage <= health.CurrentHealth
                ? bullet.Damage
                : health.CurrentHealth;

            foreach (var action in ThisMob.OnHittedActions)
                ((IExecutableAction)action).Execute();
        }
    }
}
