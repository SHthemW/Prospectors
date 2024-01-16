using Game.Interfaces;
using Game.Interfaces.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.SAction
{
    public abstract class ScriptableAction : ScriptableObject, IExecutableAction
    {
        [SerializeField]
        private SActionDataTag _responsible;

        private Dictionary<SActionDataTag, object> _runtimeKwargs;

        protected object Argument
        {
            get
            {
                if (_runtimeKwargs == null)
                    throw new ArgumentNullException(nameof(Argument));

                if (!_runtimeKwargs.TryGetValue(_responsible, out object value))
                    return null;

                return value;
            }
        }

        public IExecutableAction New(Dictionary<SActionDataTag, object> kwargs)
        {
            var type = GetType();
            var instance = CreateInstance(type);

            if (instance is ScriptableAction sa)
            {
                sa._responsible = _responsible;
                sa._runtimeKwargs = kwargs;
            }
            else throw new Exception();

            return (IExecutableAction)instance;
        }

        public abstract void Execute();
        public abstract void SetStaticArgs(in UnityEngine.Object[] objArgs, in string[] strArgs);
    }
}