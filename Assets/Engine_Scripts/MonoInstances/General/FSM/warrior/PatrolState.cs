using Game.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.General.FSM
{
    internal sealed class PatrolState : AnimationFSMState
    {
        private Rigidbody _rigidbody;

        private float   _patrolTime;
        private Vector3 _patrolDirection;

        protected override sealed void Init(Animator obj)
        {
            base.Init(obj);

            _rigidbody = GetHolderOnParent<IHoldCharRigidbody>(obj).Rigidbody;
        }

        protected override sealed void EnterStateAction()
        {
            _patrolDirection = new Vector3(
                UnityEngine.Random.Range(-1f, 1f), 
                0, 
                UnityEngine.Random.Range(-1f, 1f)
                ).normalized;

            _patrolTime = UnityEngine.Random.Range(1f, 2.5f);
        }

        protected override sealed void UpdateStateAction()
        {
            // set dir
            _rigidbody.velocity = 4 * _patrolDirection;

            _patrolTime -= Time.deltaTime;
            if (_patrolTime < 0)
                _animator.SetBool(_stateName.IdleNotPatrol, true);
        }
    }
}
