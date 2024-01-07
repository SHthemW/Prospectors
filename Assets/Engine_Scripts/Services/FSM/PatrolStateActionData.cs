using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.FSM
{
    [Serializable]
    public struct PatrolStateActionData
    {
        [SerializeField]
        private float _minPatrolTime;
        public readonly float MinPatrolTime 
            => _minPatrolTime;

        [SerializeField]
        private float _maxPatrolTime;
        public readonly float MaxPatrolTime
            => _maxPatrolTime;

        [SerializeField]
        private float _moveSpeedRatio_patrol;
        public readonly float SpeedRatioOnMove
            => _moveSpeedRatio_patrol;
    }
}
