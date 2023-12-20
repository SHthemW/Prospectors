using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public abstract class AnimationFSMState : StateMachineBehaviour
    {
        protected IAnimationStateName _stateName { get; private set; }
        protected Animator _animator { get; private set; }

        private bool _isInited   = false;
        private bool _InitFailed = false;

        public override sealed void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_isInited)
            {
                try
                {
                    Init(animator);
                }
                catch (Exception e) 
                {
                    _InitFailed = true;
                    throw e;
                }
                _isInited = true;
            }

            if (_InitFailed)
                return;

            base.OnStateEnter(animator, stateInfo, layerIndex);
            EnterStateAction();
        }

        public override sealed void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_InitFailed)
                return;

            base.OnStateUpdate(animator, stateInfo, layerIndex);
            UpdateStateAction();
        }

        public override sealed void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_InitFailed)
                return;

            base.OnStateExit(animator, stateInfo, layerIndex);
            ExitStateAction();
        }

        protected virtual void Init(Animator obj)
        {
            Debug.Log("init: " + obj.gameObject.name);

            _stateName = GetHolderOnParent<IHoldAnimStateName>(obj).AnimStateNames;
            _animator  = GetHolderOnParent<IHoldCharAnimator>(obj).Animator;
        }
        protected virtual void EnterStateAction() { }
        protected virtual void UpdateStateAction() { }
        protected virtual void ExitStateAction() { }

        protected static THold GetHolderOnParent<THold>(Animator obj, bool must = true) where THold : IDataHolder
        {
            var component = obj.GetComponentInParent<THold>();

            if (component == null && must)
                throw new MissingComponentException(typeof(THold).Name);

            return component;
        } 
    }
}
