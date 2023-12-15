using Game.Interfaces;
using Game.Utils.Extensions;
using System.Collections;
using UnityEngine;

namespace Game.Instances.Mob
{
    [CreateAssetMenu(fileName = "MobData", menuName = "Data/Mob")]
    internal sealed class MobStaticData_SO : ScriptableObject
    {
        [Header("Combat")]

        [SerializeField]
        private int _hitTimesConsumption;
        internal int HitTimesConsumption 
            => _hitTimesConsumption;

        [SerializeField]
        private int _maxHealth;
        internal int MaxHealth 
            => _maxHealth.AsSafeInspectorValue(name, hp => hp > 0);

        [Header("Action")]

        [SerializeField]
        private ExecutableAction[] _onHittedActions;
        internal ExecutableAction[] OnHittedActions 
            => _onHittedActions.AsSafeInspectorValue(name, p => p != null);

        [SerializeField]
        private bool _overrideHitActions;
        internal bool OverrideHitActions 
            => _overrideHitActions;
    }
}