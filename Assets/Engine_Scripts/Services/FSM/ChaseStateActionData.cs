using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Services.FSM
{
    [Serializable]
    public struct ChaseStateActionData
    {
        [SerializeField]
        private float _moveSpeedRatio_chase;
        public readonly float SpeedRatioOnChase
            => _moveSpeedRatio_chase;

        [SerializeField]
        private float _turnAttackDistance;
        public readonly float TurnAttackDistance
            => _turnAttackDistance;
    }
}
