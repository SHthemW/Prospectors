using Game.Interfaces;
using Game.Services.AI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.General.FSM
{
    internal sealed class IdleState : AnimationFSMState
    {
        private const string FACTOR_NAME = "idle";

        private IHoldCharMovement _charMovement;
        private float _remainIdleTime;

        private IdleStateActionData _actionData;

        protected override sealed void Init(Animator obj)
        {
            base.Init(obj);

            _charMovement = GetHolderOnParent<IHoldCharMovement>(obj);
            _actionData = GetHolderOnParent<IHoldAiActionData>(obj).Get<IdleStateActionData>();
        }

        protected override sealed void EnterStateAction()
        {
            _charMovement.MoveSpeed.AddFactor(() => 0, FACTOR_NAME);
            _remainIdleTime = UnityEngine.Random.Range(_actionData.MinIdleTime, _actionData.MaxIdleTime);
        }

        protected override sealed void UpdateStateAction()
        {
            _remainIdleTime -= Time.deltaTime;

            if (_remainIdleTime <= 0)
                _animator.SetBool(_stateName.IdleNotPatrol, false);
        }

        protected override sealed void ExitStateAction()
        {
            _charMovement.MoveSpeed.RemoveFactor(FACTOR_NAME);
        }
    }
}
