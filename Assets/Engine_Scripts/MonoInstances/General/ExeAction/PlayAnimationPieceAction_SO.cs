using Game.Interfaces;
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

        public override void Execute(in object caster)
        {
            if (caster == null)
                throw new ArgumentNullException();

            if (caster is not Animator animator)
                throw new ArgumentException();

            animator.Play(_pieceName, _layerIndex);
        }
    }
}
