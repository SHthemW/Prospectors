using Game.Interfaces;
using Game.Interfaces.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.SAction
{
    [Serializable]
    public struct ParameterizedAction : IExecutableAction
    {
        private readonly static Checker safe = new(nameof(ParameterizedAction));

        [SerializeField]
        private ScriptableAction _behaviour;

        [SerializeField]
        private UnityEngine.Object[] _objectArgs;

        [SerializeField]
        private string[] _stringArgs;

        public readonly Dictionary<SActionDataTag, object> RuntimeKwargs
        {
            get => _behaviour.RuntimeKwargs;
            set => _behaviour.RuntimeKwargs = value ?? throw new ArgumentNullException(nameof(RuntimeKwargs));
        }

        public readonly void Execute()
        {
            _behaviour.SetStaticArgs(
                objArgs: safe.Checked(_objectArgs),
                strArgs: safe.Checked(_stringArgs));

            _behaviour.Execute();
        }
    }
}
