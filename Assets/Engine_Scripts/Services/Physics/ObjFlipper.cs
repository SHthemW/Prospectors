using System;
using System.Collections;
using UnityEngine;

namespace Game.Services.Physics
{
    public sealed class ObjFlipper
    {
        private readonly Transform _transform;

        public ObjFlipper(Transform transform)
        {
            _transform = transform != null ? transform : throw new ArgumentNullException(nameof(transform));
        }
        public void SetFlipState(bool leftCond, bool rightCond = true)
        {
            float flipX = _transform.localScale.x;

            if (leftCond)
                flipX = Mathf.Abs(_transform.localScale.x) * -1;

            else if (rightCond)
                flipX = Mathf.Abs(_transform.localScale.x) * 1;

            _transform.localScale = new Vector3(flipX, _transform.localScale.y, _transform.localScale.z);
        }

        private ObjFlipper() { }
    }
}