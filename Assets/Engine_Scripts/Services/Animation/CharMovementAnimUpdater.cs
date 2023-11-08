using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Animation
{
    public sealed class CharMovementAnimUpdater
    {
        private readonly Animator _animator;
        private readonly AnimPropertyNameData_SO _property;

        public CharMovementAnimUpdater(Animator animator, AnimPropertyNameData_SO property)
        {
            _animator = animator != null ? animator : throw new ArgumentNullException(nameof(animator));
            _property = property != null ? property : throw new ArgumentNullException(nameof(property));
        }
        public void UpdateAnim(float currentVelocity)
        {
            _animator.SetFloat(_property.CurrentVelocity, currentVelocity);
        }

        private CharMovementAnimUpdater() { }
    }
}
