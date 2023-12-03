using Game.Interfaces;
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

        public override void Execute(in object caster = null)
        {
            Debug.Log("[Action test]: " + _logContent);
        }
    }
}