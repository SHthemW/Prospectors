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
        private CharAimAnimUpdater _aimAnimUpdater;

        private void Awake()
        {
            _moveAnimUpdater = new(Components.CharAnimator, DataHandler.AnimPropNames);
            _aimAnimUpdater = new(Components.AimPoint.transform, Components.AimBone, DataHandler.AimHeight);
        }

        private void Update()
        {
            _moveAnimUpdater.UpdateAnim(Components.PlayerRb.velocity.magnitude);
            _aimAnimUpdater.UpdateAimBone();
        }
    }
}