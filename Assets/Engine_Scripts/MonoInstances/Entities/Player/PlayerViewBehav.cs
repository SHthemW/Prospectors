using Game.Interfaces.GameObj;
using Game.Services.Animation;
using Game.Services.Combat;
using System.Collections;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerViewBehav : PlayerBehaviour
    {
        private CharMovementAnimUpdater _moveAnimUpdater;

        private void Awake()
        {
            _moveAnimUpdater = new(Components.CharAnimators, Components.AnimPropNames);
        }

        private void Update()
        {
            _moveAnimUpdater.UpdateAnim(
                currentVelocity: Components.PlayerRb.velocity.magnitude,
                isBackward: !CurrentMoveAndAimingIsInSameDirection()
                );
        }

        private bool CurrentMoveAndAimingIsInSameDirection()
        {
            var keyInputIsLeft  = this.DataHandler.CurrentKeyInputDirIsLeft;
            var aimingDirIsLeft = this.Components.AimPoint.transform.position.x < transform.position.x;

            return keyInputIsLeft == aimingDirIsLeft;
        }
    }
}