using System;
using UnityEngine;

namespace Game.Utils.Collections
{
    public sealed class BufferedComponent<TComponent> where TComponent : class
    {
        private readonly GameObject _objectToGetComponent;

        private TComponent _buffer;

        public BufferedComponent(GameObject objectToGetComponent)
        {
            _objectToGetComponent = objectToGetComponent != null ? objectToGetComponent : throw new ArgumentNullException(nameof(objectToGetComponent));
        }
        public BufferedComponent(TComponent buffer)
        {
            _buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
        }

        public TComponent Get()
        {
            if (_buffer != null)
                return _buffer;

            if (_objectToGetComponent == null)
                throw new Exception($"[data] {nameof(BufferedComponent<TComponent>)} is not init.");

            if (!_objectToGetComponent.TryGetComponent(out TComponent instance))
                throw new MissingComponentException(typeof(TComponent).Name);

            _buffer = instance;
            return instance;
        }
    }
}
