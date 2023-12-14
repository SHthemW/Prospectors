using Game.Interfaces;
using Game.Interfaces.GameObj;
using Game.Services.Combat;
using System.Collections.Generic;

namespace Game.Instances.Mob
{
    internal sealed class MobHitableBehav : MobBehaviour, IBulletHitable
    {
        private Dictionary<string, object>           _mobActionImpl;
        private ObjectSpawner<IDestoryManagedObject> _hitEffectSpawner;
        private ObjectSpawner<IDestoryManagedObject> _hitHoleSpawner;

        private void Awake()
        {
            _hitEffectSpawner = new();
            _hitHoleSpawner = new();
        }

        private void Start()
        {
            _mobActionImpl = new()
            {
                ["hitEffectSpawnInfo"] = (
                parent: ThisMob.HitEffectParent.Get(),
                caster: transform,
                pool: _hitEffectSpawner
                ),
                ["hitHoleSpawnInfo"] = (
                parent: ThisMob.HitHoleParent.Get(),
                caster: transform,
                pool: _hitHoleSpawner
                )
            };
        }

        int IBulletHitable.HitTimesConsumption => ThisMob.HitTimesConsumption;
        void IBulletHitable.Hit(IBullet bullet)
        {
            foreach (var action in ThisMob.OnHittedActions)
            {
                action.Implement(_mobActionImpl);
            }
        }
    }
}
