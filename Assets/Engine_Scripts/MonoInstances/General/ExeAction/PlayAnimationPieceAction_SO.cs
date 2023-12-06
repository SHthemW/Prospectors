﻿using Game.Interfaces;
using System;
using UnityEngine;

namespace Game.Instances.General
{
    [CreateAssetMenu(
        fileName = "new PlayAnimationPieceAction", 
        menuName = "General/ExeAction/PlayAnimationPieceAction")]
    internal sealed class PlayAnimationPieceAction_SO : ExecutableAction
    {
        [SerializeField]
        private string _pieceName;

        [SerializeField]
        private int _layerIndex = 0;

        protected override bool MustHaveArgument => true;
        protected override void Execute(in object animComponent)
        {
            if (animComponent == null)
                throw new ArgumentNullException();

            else if (animComponent is Animator animator)
                animator.Play(_pieceName, _layerIndex);

            else if (animComponent is Animator[] animators)
                Array.ForEach(animators, a => a.Play(_pieceName, _layerIndex));

            else
                throw new InvalidOperationException();
        }
    }
}
