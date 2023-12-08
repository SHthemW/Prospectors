using Game.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BulletHitTest : MonoBehaviour, IBulletHitable
{
    int IBulletHitable.HitTimesConsumption => 1;
    void IBulletHitable.Hit(IBullet bullet)
    {
        Debug.Log($"hitted by {bullet}");
    }
}
