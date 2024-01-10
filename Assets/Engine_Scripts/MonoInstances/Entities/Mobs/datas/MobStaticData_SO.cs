using Game.Interfaces;
using UnityEngine;

namespace Game.Instances.Mob
{
    [CreateAssetMenu(fileName = "MobData", menuName = "Data/Mob")]
    internal sealed class MobStaticData_SO : ScriptableObject
    {
        private readonly static Checker safe = new(nameof(MobStaticData_SO));

        [Header("Basic")]

        [SerializeField]
        private float _moveSpeed;
        internal float MoveSpped 
            => _moveSpeed;

        [Header("Combat")]

        [SerializeField]
        private int _hitTimesConsumption;
        internal int HitTimesConsumption 
            => _hitTimesConsumption;

        [SerializeField]
        private int _maxHealth;
        internal int MaxHealth 
            => safe.Checked(_maxHealth, static h => h > 0);

        [Header("Action")]

        [SerializeField]
        private ExecutableAction[] _onHittedActions;
        internal ExecutableAction[] OnHittedActions 
            => safe.Checked(_onHittedActions);

        [SerializeField]
        private bool _overrideHitActions;
        internal bool OverrideHitActions 
            => _overrideHitActions;
    }
}