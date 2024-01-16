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

        public readonly void Init(Dictionary<SActionDataTag, object> kwargs)
        {
            _behaviour.Init(kwargs);

            _behaviour.SetStaticArgs(
                objArgs: safe.Checked(_objectArgs),
                strArgs: safe.Checked(_stringArgs));
        }

        public readonly void Execute()
        {            
            _behaviour.Execute();
        }
    }
}
