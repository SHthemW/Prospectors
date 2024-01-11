using System.Collections.Generic;
using UnityEngine;


namespace Game.Interfaces
{
    public abstract class ScriptableAction : ScriptableObject
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