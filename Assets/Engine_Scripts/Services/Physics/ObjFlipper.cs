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
        public void SetFlipState(bool? dirIsLeft)
        {
            if (dirIsLeft == null)
                return;
            var targetFlipX = Mathf.Abs(_transform.localScale.x) * ((bool)dirIsLeft ? -1 : 1);
            _transform.localScale = new Vector3(targetFlipX, _transform.localScale.y, _transform.localScale.z);
        }

        private ObjFlipper() { }
    }
}