using Game.Interfaces;
using UnityEngine;

public sealed class BulletHitTest : MonoBehaviour, IBulletHitable
{
    public bool Enable { get; set; } = true;

    int IBulletHitable.HitTimesConsumption => 1;

    void IBulletHitable.Hit(IBullet bullet, Vector3 position)
    {
        Debug.Log($"hitted by {bullet}");
    }
}
