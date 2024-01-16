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
                    throw new ArgumentNullException();

                if (!_runtimeKwargs.TryGetValue(_responsible, out object value))
                    return null;

                return value;
            }
        }

        public void Init(Dictionary<SActionDataTag, object> kwargs)
        {
            _runtimeKwargs = kwargs;
        }

        public abstract void Execute();
        public abstract void SetStaticArgs(in UnityEngine.Object[] objArgs, in string[] strArgs);
    }
}