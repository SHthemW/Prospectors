using Game.Interfaces;
using Game.Services.Physics;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobDataAndComponentHandler : MonoBehaviour
    {
        // static datas

        [SerializeField]
        private MobStaticData_SO _staticData;

        internal int HitTimesConsumption => _staticData.HitTimesConsumption;
        internal ExecutableAction[] OnHittedActions => _staticData.OnHittedActions;

        // components ref
        public SingletonComponent<Transform> HitEffectParent { get; set; } = new("@HitEffects");
        public SingletonComponent<Transform> HitHoleParent { get; set; } = new("@HitHoles");
    }
}
