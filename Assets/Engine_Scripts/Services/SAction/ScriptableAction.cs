using Game.Interfaces;
using Game.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Services.SAction
{
    public abstract class ScriptableAction : ScriptableObject, IExecutableAction
    {
        [SerializeField]
        private SActionDataTag _responsibles;

        public Dictionary<SActionDataTag, object> RuntimeKwargs { get; set; }   

        public abstract void SetStaticArgs(in UnityEngine.Object[] objArgs, in string[] strArgs);
        public void Execute()
        {
            if (RuntimeKwargs == null)
                throw new ArgumentNullException(nameof(RuntimeKwargs));

            var resp = _responsibles.Selections();

            if (resp.Count() == 0)
                Debug.LogWarning($"[action]: {name} has no responsibles.");

            foreach (var tag in resp)
                ExecuteFor(RuntimeKwargs[tag]);
        }

        protected abstract void ExecuteFor(in object runtimeArgs);
    }
}