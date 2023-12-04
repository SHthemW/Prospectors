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
            in Animator[] animators = null
        )
        {
            switch (ActionType)
            {
                case ActionType.RequireNothing:
                    Execute();
                    break;

                case ActionType.RequireAnimator:
                    if (animators == null)
                        throw new ArgumentNullException();

                    foreach (var animator in animators)
                        if (animator.gameObject.activeInHierarchy && animator.enabled) 
                            Execute(animator);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}