using Game.Services.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerTransformBehav : PlayerBehaviour
    {           
        private RbTransformer   _movementCtrller;   
        private ObjFlipper      _faceFlipCtrller;
        private ObjActiveTurner _modelDirCtrller;

        private void Awake()
        {
            _movementCtrller = new(this.Components.PlayerRb);
            _faceFlipCtrller = new(this.Components.RootTransform);
            _modelDirCtrller = new(this.Components.PlayerModels);
        }

        private void FixedUpdate()
        {
            _movementCtrller.MoveInDirection(
                this.DataHandler.CurrentInputDirection,
                this.DataHandler.CurrentMoveSpeed);

            _faceFlipCtrller.SetFlipState(
                dirIsLeft: CalcCurrentFacingDirection_isLeft());

            _modelDirCtrller.EnableActiveOfObjectExclusively(
                key: CalcCurrentFacingDirection_isDown()
                ? "Back"
                : "Front"
                );
        }

        private bool CalcCurrentFacingDirection_isLeft()
        {
            var keyInputIsLeft = this.DataHandler.CurrentKeyInputDirIsLeft;
            var mousePosIsLeft = this.Components.AimPoint.transform.position.x < transform.position.x;

            if (keyInputIsLeft == null)
                return mousePosIsLeft; 
            else 
                return keyInputIsLeft.Value;
        }
        private bool CalcCurrentFacingDirection_isDown()
        {
            bool mousePosIsDown = this.Components.AimPoint.transform.position.z > transform.position.z;
            return mousePosIsDown;
        }
    }
}


