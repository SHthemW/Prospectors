using Game.Services.Animation;
using Game.Services.Combat;
using System.Collections;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerViewBehav : PlayerBehaviour
    {
        private CharMovementAnimUpdater _moveAnimUpdater;
        private CharAimTurnUpdater _aimTurnUpdater;

        private void Awake()
        {
            Components.AimPoint.Init(Components.RootTransform, DataHandler.MaxFollowOffsetDuringAim);

            _moveAnimUpdater = new(Components.CharAnimator, DataHandler.AnimPropNames);
            _aimTurnUpdater = new(Components.AimPoint, Components.AimBone, DataHandler.AimHeight);
        }

        private void Update()
        {
            _moveAnimUpdater.UpdateAnim(Components.PlayerRb.velocity.magnitude);
            _aimTurnUpdater.UpdateAimTurn();
        }
    }
}