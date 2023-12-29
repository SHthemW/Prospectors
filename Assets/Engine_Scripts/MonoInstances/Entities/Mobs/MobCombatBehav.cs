using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobCombatBehav : MobBehaviour
    {
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

            ThisMob.Animator.SetTrigger(ThisMob.AnimStateNames.FoundTarget);
        }

        private void OnMissedPlayer(Collider player) 
        {
            if (player == null || !player.gameObject.CompareTag("Player"))
                throw new ArgumentException(player.name);

            ThisMob.Animator.SetTrigger(ThisMob.AnimStateNames.LostTarget);
        }

    }
}
