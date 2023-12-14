using Game.Interfaces;
using Game.Utils.Extensions;
using System.Collections;
using UnityEngine;

namespace Game.Instances.Mob
{
    [CreateAssetMenu(fileName = "MobData", menuName = "Data/Mob")]
    internal sealed class MobStaticData_SO : ScriptableObject
    {
        [SerializeField]
        private int _hitTimesConsumption;
        internal int HitTimesConsumption 
            => _hitTimesConsumption;

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