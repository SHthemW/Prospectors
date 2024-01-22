using Game.Interfaces;
using Game.Interfaces.Data;
using Game.Interfaces.GameObj;
using Game.Services.Combat;
using System;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobSActionManager : MobBehaviour
    {
        private void Start()
        {
            IExecutableAction.BatchInit(kwargs: new()
            {
                [SActionDataTag.RootGameObject] = ThisMob.RootTransform.gameObject,

                [SActionDataTag.PrimaryAnimator] = ThisMob.Animator,

                [SActionDataTag.HitEffectSpawnInfo] = (
                parent: ThisMob.HitEffectParent.Get(),
                position: (Func<Vector3>)   (() => GetComponent<IHoldCharHitPosition>().CurrentHittedPosition),
                rotation: (Func<Quaternion>)(() => transform.rotation),
                pool: new ObjectSpawner<IDestoryManagedObject>()
                ),
                [SActionDataTag.HitHoleSpawnInfo] = (
                parent: ThisMob.HitHoleParent.Get(),
                position: (Func<Vector3>)   (() => GetComponent<IHoldCharHitPosition>().CurrentHittedPosition),
                rotation: (Func<Quaternion>)(() => transform.rotation),
                pool: new ObjectSpawner<IDestoryManagedObject>()
                )
            },
            ThisMob.OnHittedActions,
            ThisMob.OnDeadActions);
        }
    }
}
