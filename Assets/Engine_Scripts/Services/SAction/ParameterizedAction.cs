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

        public readonly IExecutableAction New(Dictionary<SActionDataTag, object> kwargs)
        {
            var instance = new ParameterizedAction
            {
                _behaviour = (ScriptableAction)((IExecutableAction)_behaviour).New(kwargs),
                _objectArgs = _objectArgs,
                _stringArgs = _stringArgs
            };

            instance._behaviour.SetStaticArgs(
                objArgs: safe.Checked(_objectArgs),
                strArgs: safe.Checked(_stringArgs));

            return instance;
        }

        public readonly void Execute()
        {            
            _behaviour.Execute();
        } 
    }
}
