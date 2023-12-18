using Game.Services.Physics;
using System;
using System.Collections.Generic;

namespace Game.Instances.Mob
{
    internal sealed class MobTransformBehav : MobBehaviour
    {
        private ObjFlipper _faceFlipCtrller;

        private void Awake()
        {
            _faceFlipCtrller = new(ThisMob.RootTransform);
        }

        private void Update()
        {
            _faceFlipCtrller.SetFlipState(dirIsLeft: ThisMob.Rigidbody.velocity.x > 1);
        }
    }
}
