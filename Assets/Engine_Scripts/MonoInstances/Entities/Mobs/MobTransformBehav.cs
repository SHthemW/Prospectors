using Game.Interfaces;
using Game.Services.Physics;
using Game.Utils.Collections;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobTransformBehav : MobBehaviour, IHoldCharMovement
    {
        /*
         *  datas
         */

        [field: Header("Datas")]

        [field: SerializeField, Utils.Attributes.ReadOnly]
        public DynamicData<float> MoveSpeed { get; set; } = new(
            howToMerge: (f1, f2) => f1 * f2,
            factorBase: 1
            );
        
        [field: SerializeField, Utils.Attributes.ReadOnly]
        public Vector3 MoveDirection { get; set; }

        /*
         *  behaviours
         */

        private ObjFlipper    _faceFlipCtrller;
        private RbTransformer _movementCtrller;

        private void Awake()
        {
            MoveSpeed.Init(ThisMob.BaseMoveSpeed);

            _faceFlipCtrller = new(ThisMob.RootTransform);
            _movementCtrller = new(ThisMob.Rigidbody);
        }

        private void FixedUpdate()
        {
            _faceFlipCtrller.SetFlipState(
                leftCond: ThisMob.Rigidbody.velocity.x > 1,
                rightCond: ThisMob.Rigidbody.velocity.x < -1);

            _movementCtrller.MoveInDirection(
                direction: MoveDirection,
                speed: MoveSpeed.UpdateCurrentAndGet());
        }
    }
}
