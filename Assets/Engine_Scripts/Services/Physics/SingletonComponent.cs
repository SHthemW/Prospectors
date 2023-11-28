using System;
using UnityEngine;

namespace Game.Services.Physics
{
    public sealed class SingletonComponent<TComponent> where TComponent : UnityEngine.Component
    {
        private readonly string _name;

        private TComponent _componentTemp = null;

        private const char CHECK_CHAR = '@';

        public SingletonComponent(string name)
        {
            this._name = name ?? throw new ArgumentNullException(nameof(name));

            if (!this._name.Contains(CHECK_CHAR))
                Debug.LogWarning($"[obj] you're trying to init a singleton object, but it should named by prefix {CHECK_CHAR}.");
        }
        public TComponent Get()
        {
            if (_componentTemp != null)
            {
                if (_componentTemp.gameObject.name != _name)
                    throw new Exception($"[obj] singleton object {_name} has invalid temp: name not match with it should be.");
                return _componentTemp;
            }
            _componentTemp = Create();
            return _componentTemp;
        }

        private SingletonComponent()
            => throw new NotImplementedException();
        private TComponent Create()
        {
            var obj = GameObject.Find(_name);
            if (obj != null)
                return obj.GetComponent<TComponent>();

            Debug.LogWarning(
                    $"[obj] singleton object {_name} not found, it will be create automatically by program. " +
                    $"But remember, create it by yourself is better.");
            return new GameObject(_name).GetComponent<TComponent>();
        }
    }
}
