using Game.Services.SAction;
using System;
using UnityEngine;

namespace Game.Instances.General
{
    [CreateAssetMenu(
        fileName = "new DestoryGameObjectAction",
        menuName = "General/ExeAction/DestoryGameObjectAction")]
    internal sealed class DestroyGameObjectAction_SO : ScriptableAction
    {
        private GameObject _toDestroy;

        public override sealed void SetStaticArgs(in UnityEngine.Object[] objArgs, in string[] strArgs)
        {
            switch (objArgs)
            {
                case UnityEngine.Object[] oa when oa.Length == 0:
                    break;

                case UnityEngine.Object[] oa when oa.Length == 1 && oa[0] is GameObject obj:
                    _toDestroy = obj;
                    break;

                default: 
                    throw new ArgumentException();
            }

            switch (strArgs)
            {
                case string[] sa when sa.Length == 0:
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
                    if (_toDestroy == null)
                        throw new ArgumentNullException("nothing can destory.");
                    Destroy(_toDestroy);
                    break;

                case GameObject obj:
                    if (_toDestroy != null)
                        Debug.LogWarning("destory target is already given, runtime arg will be ignore.");
                    Destroy(_toDestroy == null ? obj : _toDestroy);
                    break;

                default:
                    throw new ArgumentException(message: $"invalid runtime argument in {name}");
            }
        }
    }
}
