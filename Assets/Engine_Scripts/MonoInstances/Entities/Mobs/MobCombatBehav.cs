using Game.Interfaces;
using System;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobCombatBehav : MobBehaviour, IHoldAttackTarget
    {
        public Transform Target { get; set; }

        private void OnEnable()
        {
            ThisMob.PlayerDetector.OnCollisionEnter += OnDetectedPlayer;
            ThisMob.PlayerDetector.OnCollisionExit += OnMissedPlayer;
        }

        private void OnDisable()
        {
            ThisMob.PlayerDetector.OnCollisionEnter -= OnDetectedPlayer;
            ThisMob.PlayerDetector.OnCollisionExit -= OnMissedPlayer;
        }

        private void OnDetectedPlayer(Collider player)
        {
            if (player == null || !player.gameObject.CompareTag("Player"))
                throw new ArgumentException(player.name);

            Target = player.transform;
            ThisMob.Animator.SetTrigger(ThisMob.AnimStateNames.FoundTarget);
        }

        private void OnMissedPlayer(Collider player) 
        {
            if (player == null || !player.gameObject.CompareTag("Player"))
                throw new ArgumentException(player.name);

            Target = null;
            ThisMob.Animator.SetTrigger(ThisMob.AnimStateNames.LostTarget);
        }

    }
}
