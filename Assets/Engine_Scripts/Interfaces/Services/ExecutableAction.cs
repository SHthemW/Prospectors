using Game.Utils.Extensions;
using System;
using System.Collections;
using UnityEngine;


namespace Game.Interfaces
{
    public enum ActionType
    {
        RequireNothing, RequireAnimator
    }

    public abstract class ExecutableAction : ScriptableObject
    {
        protected abstract ActionType ActionType { get; }
        protected abstract void Execute(in object caster = null);

        public void Implement
        (
            in Animator animator = null
        )
        {
            switch (ActionType)
            {
                case ActionType.RequireNothing:
                    Execute();
                    break;

                case ActionType.RequireAnimator:
                    Execute(animator.AsSafeInspectorValue(name, a => a != null));
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}