using Game.Services.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerTransformBehav : PlayerBehaviour
    {           
        private RbTransformer _movementCtrller;
        private ObjFlipper _faceFlipCtrller;

        private void Awake()
        {
            _movementCtrller = new(this.Components.PlayerRb);
            _faceFlipCtrller = new(this.Components.RootTransform);
        }

        private void FixedUpdate()
        {
            _movementCtrller.MoveInDirection(
                this.DataHandler.CurrentInputDirection,
                this.DataHandler.CurrentMoveSpeed);

            _faceFlipCtrller.SetFlipState(
                dirIsLeft: CurrentFaceDirIsLeft());
        }

        private bool CurrentFaceDirIsLeft()
        {
            var keyInputIsLeft = this.DataHandler.CurrentKeyInputDirIsLeft;
            var mousePosIsLeft = this.Components.AimPoint.transform.position.x < transform.position.x;

            if (keyInputIsLeft == null)
                return mousePosIsLeft; 
            else 
                return keyInputIsLeft.Value;
        }
    }
}


