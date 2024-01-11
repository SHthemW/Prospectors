using Game.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Instances.General
{
    [CreateAssetMenu(
        fileName = "new DebugLogAction",
        menuName = "General/ExeAction/DebugLogAction")]
    internal sealed class DebugLogAction_SO : ExecutableAction
    {
        [SerializeField]
        private string _logContent;

        public override void TrySetArgs(in UnityEngine.Object[] objArgs, in string[] strArgs)
        {
            switch (objArgs)
            {
                case UnityEngine.Object[] oa when oa.Length == 0:
                    break;

                default:
                    throw new ArgumentException();
            }   

            switch (strArgs)
            {
                case string[] sa when sa.Length == 0:
                    break;

                case string[] sa when sa.Length == 1:
                    _logContent = sa[0]; 
                    break;

                default:
                    throw new ArgumentException();
            }
        }

        protected override void Execute(in object runtimeArgs = null)
        {
            Debug.Log("[Action test]: " + _logContent);
        }
    }
}