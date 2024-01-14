using Game.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Instances.General
{
    [CreateAssetMenu(
        fileName = "new DebugLogAction",
        menuName = "General/ExeAction/DebugLogAction")]
    internal sealed class DebugLogAction_SO : ScriptableAction
    {
        private string _logContent;

        /// <param name="objArgs">
        /// <br/> []
        /// </param>
        /// <param name="strArgs">
        /// <br/> [0. log text: string]
        /// </param>
        /// <exception cref="ArgumentException"></exception>
        public override sealed void SetStaticArgs(in UnityEngine.Object[] objArgs, in string[] strArgs)
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

        protected override sealed void ExecuteFor(in object runtimeArgs)
        {
            Debug.Log("[Action test]: " + _logContent);
        }
    }
}