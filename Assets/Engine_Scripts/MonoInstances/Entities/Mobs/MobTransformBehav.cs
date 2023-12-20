using Game.Services.Physics;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobTransformBehav : MobBehaviour
    {
        private ObjFlipper    _faceFlipCtrller;
        private RbTransformer _movementCtrller;

        private void Awake()
        {
            ThisMob.MoveSpeed.Init(ThisMob.BaseMoveSpeed);

            _faceFlipCtrller = new(ThisMob.RootTransform);
            _movementCtrller = new(ThisMob.Rigidbody);
        }

        private void Update()
        {
            _faceFlipCtrller.SetFlipState(
                leftCond:  ThisMob.Rigidbody.velocity.x > 1,
                rightCond: ThisMob.Rigidbody.velocity.x < -1);

            Debug.Log(ThisMob.MoveSpeed.UpdateCurrentAndGet());
            _movementCtrller.MoveInDirection(
                direction: ThisMob.MoveDirection, 
                speed:     ThisMob.MoveSpeed.UpdateCurrentAndGet());
        }
    }
}
