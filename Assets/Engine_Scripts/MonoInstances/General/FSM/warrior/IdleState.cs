using Game.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.General.FSM
{
    internal sealed class IdleState : AnimationFSMState
    {
        private const float IDLE_TIME = 3f;

        private float _remainIdleTime;

        protected override void EnterStateAction()
        {
            _remainIdleTime = IDLE_TIME;
        }

        protected override void UpdateStateAction()
        {
            _remainIdleTime -= Time.deltaTime;

            if (_remainIdleTime <= 0)
                _animator.SetBool(_stateName.IdleNotPatrol, false);
        }

    }
}
