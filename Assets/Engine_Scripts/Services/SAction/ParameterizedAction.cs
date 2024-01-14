using Game.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.SAction
{
    [Serializable]
    public struct ParameterizedAction
    {
        private readonly static Checker safe = new(nameof(ParameterizedAction));

        [SerializeField]
        private ScriptableAction _behaviour;
        public readonly void Execute(Dictionary<ScriptableActionTag, object> kwargs)
        {
            _behaviour.SetStaticArgs(
                objArgs: safe.Checked(_objectArgs),
                strArgs: safe.Checked(_stringArgs));

            _behaviour.RuntimeKwargs = kwargs ?? throw new ArgumentNullException(nameof(kwargs));
            _behaviour.Execute();
        }

        [SerializeField]
        private UnityEngine.Object[] _objectArgs;

        [SerializeField]
        private string[] _stringArgs;
    }
}
