using Game.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BulletHitTest : MonoBehaviour, IBulletHitable
{
    int IBulletHitable.HitTimesConsumption => 1;
    bool IBulletHitable.OverrideHitActions => false;

    void IBulletHitable.Hit(IBullet bullet, Vector3 position)
    {
        Debug.Log($"hitted by {bullet}");
    }
}
