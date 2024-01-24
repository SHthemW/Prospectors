using Game.Interfaces;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobHitableBehav : MobBehaviour, IBulletHitable, IEnableOnAliveOnly, IHoldCharHitPosition
    {
        [field: SerializeField]
        public Vector3 CurrentHittedPosition { get; set; }
        public bool Enable { get; set; } = true;

        private void Awake()
        {
            ThisMob.HitPosHolder = new(this);
        }

        int IBulletHitable.HitTimesConsumption => ThisMob.HitTimesConsumption;
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
