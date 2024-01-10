using Game.Interfaces;
using System;
using UnityEngine;

namespace Game.Services.Combat
{
    public static class CombatUtil
    {
        public static void Hit(IHoldCharHealth who, int damage, (Animator animator, IAnimationStateName name) anim = default)
        {
            if (who.CurrentHealth <= 0)
                throw new ArgumentException();

            who.CurrentHealth -= damage <= who.CurrentHealth
                ? damage
                : who.CurrentHealth;

            if (anim != default) // play animation
            {
                var (animator, name) = anim;
                animator.SetTrigger(name.OnHit[UnityEngine.Random.Range(0, name.OnHit.Length)]);
            }
        }

        public static void Die(IEnableOnAliveOnly[] behavioursToDisable, (Animator animator, IAnimationStateName name) anim = default)
        {
            if (behavioursToDisable == null)
                throw new ArgumentNullException();

            Array.ForEach(behavioursToDisable, behav => behav.Enable = false);

            if (anim != default) // play animation
            {
                var (animator, name) = anim;
                animator.SetTrigger(name.Die);
            }
        }
    }
}