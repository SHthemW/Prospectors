using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Animation
{
    public sealed class CharMovementAnimUpdater
    {
        private readonly Animator[] _animators;
        private readonly AnimPropertyNameData_SO _property;

        public CharMovementAnimUpdater(Animator animator, AnimPropertyNameData_SO property)
        {
            if (animator == null)
                throw new ArgumentNullException(nameof(animator));

            _animators = new Animator[1] { animator };
            _property = property != null ? property : throw new ArgumentNullException(nameof(property));
        }
        public CharMovementAnimUpdater(Animator[] animators, AnimPropertyNameData_SO property)
        {
            _animators = animators ?? throw new ArgumentNullException(nameof(animators));
            _property = property != null ? property : throw new ArgumentNullException(nameof(property));
        }
        public void UpdateAnim(float currentVelocity, bool isBackward = false)
        {
            foreach (var animator in _animators)
            {
                if (!animator.isActiveAndEnabled)
                    continue;

                animator.SetFloat(
                    _property.CurrentVelocity,
                    currentVelocity * (isBackward ? -1 : 1));
            }
        }

        private CharMovementAnimUpdater()
            => throw new NotImplementedException();
    }
}
