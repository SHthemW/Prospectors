﻿using Game.Interfaces;
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


        [Header("patrol")]

        [SerializeField]
        private float _minPatrolTime;
        public float MinPatrolTime => 
            _minPatrolTime;

        [SerializeField]
        private float _maxPatrolTime;
        public float MaxPatrolTime 
            => safe.Checked(_maxPatrolTime);

        [SerializeField]
        private float _moveSpeedRatio_patrol;
        public float MoveSpeedRatio_patrol
            => safe.Checked(_moveSpeedRatio_patrol);

        [Header("chase")]

        [SerializeField]
        private float _moveSpeedRatio_chase;
        public float MoveSpeedRatio_chase 
            => safe.Checked(_moveSpeedRatio_chase);

        [SerializeField]
        private float _turnAttackDistance;
        public float TurnAttackDistance
            => safe.Checked(_turnAttackDistance);
    }
}
