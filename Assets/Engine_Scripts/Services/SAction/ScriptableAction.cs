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
                    throw new ArgumentNullException();

                if (!RuntimeKwargs.TryGetValue(_responsible, out object value))
                    return null;

                return value;
            }
        }

        public abstract void Execute();
        public abstract void SetStaticArgs(in UnityEngine.Object[] objArgs, in string[] strArgs);
    }
}