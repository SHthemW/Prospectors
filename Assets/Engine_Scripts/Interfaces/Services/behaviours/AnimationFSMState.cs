using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public abstract class AnimationFSMState : StateMachineBehaviour
    {
        protected IAnimationStateName _stateName { get; private set; }
        protected Animator _animator { get; private set; }

        private bool _isInited = false;

        public override sealed void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_isInited)
            {
                Init(animator);
                _isInited = true;
            }

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

        protected virtual void Init(Animator obj)
        {
            Debug.Log("init: " + obj.gameObject.name);

            _stateName = GetDataByHolder<IAnimationStateName>(obj);
            _animator = GetDataByHolder<Animator>(obj);
        }
        protected virtual void EnterStateAction() { }
        protected virtual void UpdateStateAction() { }
        protected virtual void ExitStateAction() { }

        protected TData GetDataByHolder<TData>(Animator obj, bool must = true)
        {
            var holder = obj.GetComponentInParent<IDataHolder<TData>>();

            if (holder == null && must)
                throw new MissingComponentException(typeof(TData).Name);

            return holder.Data;
        }
    }
}
