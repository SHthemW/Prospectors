using Game.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.General.FSM
{
    internal sealed class PatrolState : AnimationFSMState
    {
        private bool    _isInPatrol = false;
        private float   _patrolTime;

        private IHoldCharMovement _charMovement;

        protected override sealed void Init(Animator obj)
        {
            base.Init(obj);

            _charMovement = GetHolderOnParent<IHoldCharMovement>(obj);

            _charMovement.MoveSpeed.AddFactor(() => _isInPatrol ? 1 : 0, "patrol");
        }

        protected override sealed void EnterStateAction()
        {
            _isInPatrol = true;
            _patrolTime = UnityEngine.Random.Range(1f, 2.5f);

            _charMovement.MoveDirection = new Vector3(
                UnityEngine.Random.Range(-1f, 1f),
                0,
                UnityEngine.Random.Range(-1f, 1f)
                ).normalized;
        }

        protected override sealed void UpdateStateAction()
        {
            _patrolTime -= Time.deltaTime;
            if (_patrolTime < 0)
                _animator.SetBool(_stateName.IdleNotPatrol, true);
        }

        protected override sealed void ExitStateAction()
        {
            _isInPatrol = false;
        }
    }
}
