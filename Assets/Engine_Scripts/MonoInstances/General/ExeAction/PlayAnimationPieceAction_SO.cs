using Game.Services.SAction;
using System;
using UnityEngine;

namespace Game.Instances.General
{
    [CreateAssetMenu(
        fileName = "new PlayAnimationPieceAction", 
        menuName = "General/ExeAction/PlayAnimationPieceAction")]
    internal sealed class PlayAnimationPieceAction_SO : ScriptableAction
    {
        private Animator _animator = null;
        private string   _pieceName;
        private int      _layerIndex = 0;

        private PlayType _playType   = 0;
        private string   _playArg    = null;
        private enum PlayType : int 
        { 
            DirectPlay = 0, SetTrigger, SetBool, SetInt, SetFloat 
        }

        /// <param name="objArgs">
        /// <br/> [0. override animator: Animator]
        /// </param>
        /// <param name="strArgs">
        /// <br/> [0. piece name: string]
        /// <br/> [1. layer index: int]
        /// <br/> [2. play type: int]
        /// <br/> [3. play argument: string]
        /// </param>
        /// <exception cref="ArgumentException"></exception>
        public override sealed void SetStaticArgs(in UnityEngine.Object[] objArgs, in string[] strArgs)
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

                        _playType = (PlayType)int.Parse(sa[2]);
                        _playArg = sa[3];
                    }
                    catch (IndexOutOfRangeException) { break; }
                    break;

                default:
                    throw new ArgumentException(message: $"invalid static argument in {name}");
            }
        }

        public override sealed void Execute()
        {
            switch (Argument)
            {
                case null:
                    if (_animator == null)
                        throw new ArgumentNullException(nameof(Argument));
                    _animator.Play(_pieceName, _layerIndex);
                    break;

                case Animator animator:
                    if (animator.gameObject.activeInHierarchy)
                        this.PlayOn(animator);
                    break;

                case Animator[] animators:
                    foreach (Animator animator in animators)
                        if (animator.gameObject.activeInHierarchy)
                            this.PlayOn(animator);
                    break;

                default:
                    throw new ArgumentException(message: $"invalid runtime argument in {name}");
            }
        }

        private void PlayOn(Animator animator)
        {
            switch (_playType)
            {
                case PlayType.DirectPlay:
                    animator.Play(_pieceName, _layerIndex);
                    break;

                case PlayType.SetTrigger:
                    animator.SetTrigger(_pieceName);
                    break;

                case PlayType.SetFloat:
                    animator.SetFloat(_pieceName, float.Parse(_playArg));
                    break;

                case PlayType.SetBool:
                    animator.SetBool(_pieceName, bool.Parse(_playArg));
                    break;

                case PlayType.SetInt:
                    animator.SetInteger(_pieceName, int.Parse(_playArg));
                    break;

                default:
                    throw new InvalidOperationException("cannot attach there.");
            }
        }
    }
}
