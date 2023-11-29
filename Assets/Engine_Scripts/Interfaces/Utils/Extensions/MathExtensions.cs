using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils.Extensions
{
    public static class MathExtensions
    {
        public static Vector3 RotateAloneAxisY(this Vector3 dir, float clockwiseAngle)
        {
            // 创建一个绕Y轴旋转offset度的四元数
            Quaternion rotation = Quaternion.Euler(0, clockwiseAngle, 0);
            // 使用这个四元数旋转dir向量
            Vector3 newDir = rotation * dir;
            return newDir;
        }
    }
}
