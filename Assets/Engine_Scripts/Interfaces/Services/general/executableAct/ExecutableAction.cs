using Game.Utils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;


namespace Game.Interfaces
{
    public abstract class ExecutableAction : ScriptableObject
    {
        [SerializeField]
        private string[] _effectiveTags;

        protected abstract void Execute(in object runtimeArgs = null);

        public abstract void TrySetArgs(in UnityEngine.Object[] objArgs, in string[] strArgs);
        public void Implement(in Dictionary<string, object> kwargs = null)
        {
            if (_effectiveTags.Length == 1 && _effectiveTags[0] == "ALL")
                Execute();

            foreach (var tag in _effectiveTags)
                Execute(kwargs[tag]);
        }
    }
}