using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    [Serializable]
    public struct ParameterizedAction
    {
        private readonly static Checker safe = new(nameof(ParameterizedAction));

        [SerializeField]
        private ExecutableAction _behaviour;
        public readonly void Implement(Dictionary<string, object> kwargs = null)
        {
            _behaviour.TrySetArgs(
                objArgs: safe.Checked(_objectArgs),
                strArgs: safe.Checked(_stringArgs));

            _behaviour.Implement(kwargs);
        }

        [SerializeField]
        private UnityEngine.Object[] _objectArgs;

        [SerializeField]
        private string[] _stringArgs;
    }
}
