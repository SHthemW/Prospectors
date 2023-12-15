using Game.Interfaces;
using System.Collections;
using UnityEngine;

namespace Game.Instances.Mob
{
    [CreateAssetMenu(fileName = "Default Brain", menuName = "Data/MobBrain/Default")]
    internal class MobBrain_SO : ScriptableObject, IMobBrain
    {
        public virtual void Hit(IMob mob, int damage)
        {
            mob.CurrentHealth -= damage;
            mob.Animator.SetTrigger(mob.AnimNames.OnHit);
        }
    }
}