using Game.Interfaces.GameObj;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Services.Combat
{
    public sealed class AimPoint : MonoBehaviour, IDynamicPoint
    {
        private Transform _rootTransform;
        private Vector2 _maxOffset;
        Vector3 IDynamicPoint.Position => transform.position;

        public void Init(Transform rootTransform, Vector2 maxOffset)
        {
            _rootTransform = rootTransform != null ? rootTransform : throw new ArgumentNullException(nameof(rootTransform));
            _maxOffset = maxOffset;
        }

        private void Update()
        {
            transform.position = GetOffsetPosition() + _rootTransform.position;
        }
        private Vector3 GetOffsetPosition()
        {
            var offsetIn2D = GetOffsetRatioInEachAxis() * _maxOffset;
            return new Vector3(offsetIn2D.x, 0, offsetIn2D.y);

            static Vector2 GetOffsetRatioInEachAxis()
            {
                var mousePos = Input.mousePosition;

                int x = Screen.width, half_w = x / 2;
                int y = Screen.height, half_h = y / 2;

                return new Vector2(
                    (mousePos.x - half_w) / half_w,
                    (mousePos.y - half_h) / half_h
                );
            }
        }

    }
}