using Game.Interfaces;
using System;
using UnityEngine;

namespace Game.Services.Animation
{
    public sealed class CharMovementAnimUpdater
    {
        private readonly Animator[]          _animators;
        private readonly IAnimationStateName _animNames;

        public CharMovementAnimUpdater(Animator animator, IAnimationStateName property)
        {
            if (animator == null)
                throw new ArgumentNullException(nameof(animator));

            _animators = new Animator[1] { animator };
            _animNames = property != null ? property : throw new ArgumentNullException(nameof(property));
        }
        public CharMovementAnimUpdater(Animator[] animators, IAnimationStateName property)
        {
            _animators = animators ?? throw new ArgumentNullException(nameof(animators));
            _animNames = property != null ? property : throw new ArgumentNullException(nameof(property));
        }
        public void UpdateAnim(float currentVelocity, bool isBackward = false)
        {
            foreach (var animator in _animators)
            {
                if (!animator.isActiveAndEnabled)
                    continue;

                animator.SetFloat(
                    _animNames.CurrentVelocity,
                    currentVelocity * (isBackward ? -1 : 1));
            }
        }

        private CharMovementAnimUpdater()
            => throw new NotImplementedException();
    }
}
