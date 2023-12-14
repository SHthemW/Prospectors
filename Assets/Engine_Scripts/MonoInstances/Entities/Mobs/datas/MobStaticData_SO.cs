using Game.Interfaces;
using System.Collections;
using UnityEngine;

namespace Game.Instances.Mob
{
    [CreateAssetMenu(fileName = "MobData", menuName = "Data/Mob")]
    internal sealed class MobStaticData_SO : ScriptableObject
    {
        [SerializeField]
        private int _hitTimesConsumption;
        public int HitTimesConsumption => _hitTimesConsumption;

        [field: SerializeField]
        public ExecutableAction[] OnHittedActions { get; set; }
    }
}