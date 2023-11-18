using Game.Services.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerTransformBehav : PlayerBehaviour
    {           
        private RbTransformer           _movementCtrller;   
        private ObjFlipper              _faceFlipCtrller;
        private ObjActiveTurner         _modelDirCtrller;
        private ObjPositionSynchronizer _handAndWeaponPosSyncr;

        private void Awake()
        {
            _movementCtrller = new(this.Components.PlayerRb);

            _faceFlipCtrller = new(this.Components.RootTransform);

            _modelDirCtrller = new(this.Components.PlayerModels);

            _handAndWeaponPosSyncr = new
            (
                syncTarget: transform,
                toBeSync:   Components.CharacterHands
            );
        }

        private void FixedUpdate()
        {
            _movementCtrller.MoveInDirection(
                this.DataHandler.CurrentInputDirection,
                this.DataHandler.CurrentMoveSpeed);

            _faceFlipCtrller.SetFlipState(
                dirIsLeft: CurrentAimingPosIsInLeft());

            _modelDirCtrller.EnableActiveOfObjectExclusively(
                key: CalcCurrentFacingDirection_isDown()
                ? "Back"
                : "Front"
                );

            _handAndWeaponPosSyncr.Sync();
        }

        private bool CurrentAimingPosIsInLeft()
        {
            return this.Components.AimPoint.transform.position.x < transform.position.x;
        }
        private bool CalcCurrentFacingDirection_isDown()
        {
            bool mousePosIsDown = this.Components.AimPoint.transform.position.z > transform.position.z;
            return mousePosIsDown;
        }
    }
}


