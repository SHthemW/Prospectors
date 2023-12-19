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
            if (mob.CurrentHealth <= 0)
                throw new System.ArgumentException();

            mob.CurrentHealth -= damage <= mob.CurrentHealth 
                ? damage 
                : mob.CurrentHealth;

            mob.Animator.SetTrigger(mob.AnimNames.OnHit[Random.Range(0, mob.AnimNames.OnHit.Length)]);
        }

        public void Die(IMob mob)
        {
            mob.gameObject.SetActive(false);
        }
    }
}