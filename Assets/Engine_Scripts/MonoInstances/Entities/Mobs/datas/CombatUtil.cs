using Game.Interfaces;
using System.Collections;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal static class CombatUtil
    {
        public static void Hit(IHoldCharHealth who, int damage, (Animator animator, IAnimationStateName name) anim = default)
        {
            if (who.CurrentHealth <= 0)
                throw new System.ArgumentException();

            who.CurrentHealth -= damage <= who.CurrentHealth
                ? damage
                : who.CurrentHealth;

            if (anim != default) // play animation
            {
                var (animator, name) = anim;
                animator.SetTrigger(name.OnHit[Random.Range(0, name.OnHit.Length)]);
            }
        }

        public static void Die(IGameObject character)
        {
            character.gameObject.SetActive(false);
        }
    }
}