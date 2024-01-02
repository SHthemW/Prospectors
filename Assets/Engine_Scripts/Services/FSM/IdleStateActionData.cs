using Game.Interfaces;
using System;
using UnityEngine;

namespace Game.Services.FSM
{
    [Serializable]
    public struct IdleStateActionData
    {
        [SerializeField]
        private float _minIdleTime;
        public readonly float MinIdleTime
            => _minIdleTime;

        [SerializeField]
        private float _maxIdleTime;
        public readonly float MaxIdleTime
            => _maxIdleTime;
    }
}
