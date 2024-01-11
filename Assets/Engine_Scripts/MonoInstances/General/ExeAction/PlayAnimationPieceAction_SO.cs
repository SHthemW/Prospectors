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
        private Animator _animator = null;
        private string   _pieceName;
        private int      _layerIndex = 0;

        /// <param name="objArgs">
        /// <br/> 0. override animator: Animator
        /// </param>
        /// <param name="strArgs">
        /// <br/> [0. piece name: string]
        /// <br/> [1. layer index: int]
        /// </param>
        /// <exception cref="ArgumentException"></exception>
        public override void TrySetArgs(in UnityEngine.Object[] objArgs, in string[] strArgs)
        {
            switch (objArgs) 
            { 
                case UnityEngine.Object[] oa when oa.Length == 0:
                    break;

                case UnityEngine.Object[] oa when oa.Length == 1 && oa[0] is Animator animator:
                    _animator = animator;
                    break;

                default:
                    throw new ArgumentException();
            }

            switch (strArgs)
            {
                case string[] sa when sa.Length == 0:
                    break;

                case string[] sa:
                    try
                    {
                        _pieceName = sa[0];
                        _layerIndex = int.Parse(sa[1]);
                    }
                    catch (IndexOutOfRangeException) { break; }
                    break;

                default:
                    throw new ArgumentException();
            }
        }

        protected override void Execute(in object runtimeArgs = null)
        {
            switch (runtimeArgs)
            {
                case null:
                    if (_animator == null)
                        throw new ArgumentNullException(nameof(runtimeArgs));
                    _animator.Play(_pieceName, _layerIndex);
                    break;

                case Animator animator:
                    if (animator.gameObject.activeInHierarchy)
                        animator.Play(_pieceName, _layerIndex);
                    break;

                case Animator[] animators:
                    foreach (Animator each in animators)
                        if (each.gameObject.activeInHierarchy)
                            each.Play(_pieceName, _layerIndex);
                    break;

                default:
                    throw new ArgumentException();
            }
        }
    }
}
