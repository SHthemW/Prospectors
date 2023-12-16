using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public abstract class AnimationFSMState : StateMachineBehaviour
    {
        protected IAnimationStateName _stateName { get; private set; }
        protected Animator _animator { get; private set; }

        public override sealed void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            TryInitComponent(animator);

            base.OnStateEnter(animator, stateInfo, layerIndex);
            EnterStateAction();
        }

        public override sealed void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            UpdateStateAction();
        }

        public override sealed void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            ExitStateAction();
        }

        protected virtual void EnterStateAction() { }
        protected virtual void UpdateStateAction() { }
        protected virtual void ExitStateAction() { }

        private bool _isInited = false;
        private void TryInitComponent(Animator animator)
        {
            if (_isInited)
                return;

            Debug.Log("init: " + animator.gameObject.name);

            _stateName = (animator
                 . GetComponentInParent<IDataHolder<IAnimationStateName>>()
                ?? throw new MissingComponentException(nameof(_stateName)))
                 . Data;

            _animator = (animator
                 . GetComponentInParent<IDataHolder<Animator>>()
                ?? throw new MissingComponentException(nameof(_animator)))
                 . Data;

            _isInited = true;
        }
    }
}
