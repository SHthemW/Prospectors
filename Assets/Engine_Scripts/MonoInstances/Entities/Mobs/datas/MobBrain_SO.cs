using Game.Interfaces;
using System.Collections;
using UnityEngine;

namespace Game.Instances.Mob
{
    [CreateAssetMenu(fileName = "Default Brain", menuName = "Data/MobBrain/Default")]
    internal class MobBrain_SO : ScriptableObject, IMobCombatBrain
    {
        public virtual void Init(IMob mob)
        {
            mob.CurrentHealth = mob.MaxHealth;
        }

        public virtual void Hit(IMob mob, int damage)
        {
            mob.CurrentHealth -= damage;
            mob.Animator.SetTrigger(mob.AnimNames.OnHit);
        }
    }
}