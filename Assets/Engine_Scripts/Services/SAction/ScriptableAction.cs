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

        public Dictionary<SActionDataTag, object> RuntimeKwargs { get; set; }
        protected object Argument
        {
            get
            {
                if (RuntimeKwargs == null)
                    throw new ArgumentNullException(nameof(Argument), message: $"SAction {name} has null runtime argument.");

                if (!RuntimeKwargs.TryGetValue(_responsible, out object value))
                    return null;

                return value;
            }
        }

        public IExecutableAction DeepClone()
        {
            var instance = CreateInstance(GetType());

            if (instance is ScriptableAction sa)
            {
                sa._responsible = _responsible;
            }
            else throw new Exception();

            return (ScriptableAction)instance;
        }

        public abstract void Execute();
        public abstract void SetStaticArgs(in UnityEngine.Object[] objArgs, in string[] strArgs);
    }
}