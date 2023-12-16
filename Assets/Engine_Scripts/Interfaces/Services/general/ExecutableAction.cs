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

        protected abstract bool MustHaveArgument { get; }
        protected abstract void Execute(in object caster = null);

        public void Implement(in Dictionary<string, object> kwargs = null)
        {
            if (_effectiveTags.Length == 0 || kwargs.Count == 0)
            {
                if (MustHaveArgument)
                    throw new ArgumentException();
                else
                    Execute();
            }
            else 
                foreach (var tag in _effectiveTags)
                    Execute(kwargs[tag]);
        }
    }
}