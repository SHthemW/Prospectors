using Game.Interfaces;
using UnityEngine;

namespace Game.Services.FSM
{
    [CreateAssetMenu(fileName = "new Warrior", menuName = "Data/FSM/Warrior")]
    public sealed class WarriorActionData_SO : FSMActionData
    {
        private static readonly Checker safe = new(nameof(WarriorActionData_SO));

        [SerializeField]
        private IdleStateActionData _idle;
        public IdleStateActionData Idle
            => safe.Checked(_idle);

        [SerializeField]
        private PatrolStateActionData _patrol;
        public PatrolStateActionData Patrol
            => safe.Checked(_patrol);

        [SerializeField]
        private ChaseStateActionData _chase;
        public ChaseStateActionData Chase
            => safe.Checked(_chase);
    }
}
