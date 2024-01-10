using System;
using UnityEngine;

namespace Game.Utils.Collections
{
    [Serializable]
    public struct ObjectComponent<TComponent> where TComponent : class
    {
        [SerializeField]
        private GameObject _object;

        private TComponent _buffer;

        public TComponent Get()
        {
            if (_buffer != null)
                return _buffer;

            if (_object == null)
                throw new Exception($"[data] {nameof(ObjectComponent<TComponent>)} is not init.");

            if (!_object.TryGetComponent(out TComponent instance))
                throw new MissingComponentException(typeof(TComponent).Name);

            _buffer = instance;
            return instance;
        }
    }
}
