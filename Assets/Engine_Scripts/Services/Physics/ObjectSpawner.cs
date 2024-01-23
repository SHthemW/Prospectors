using Game.Interfaces.GameObj;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Services.Combat
{
    internal sealed class ObjectSpawner<TComponent> where TComponent : IDestoryManagedObject
    {
        private readonly ObjectPool<GameObject>             _gameObjectPool;
        private readonly Dictionary<GameObject, TComponent> _componentPool;

        private readonly GameObject _spawn;

        internal ObjectSpawner(in GameObject spawn)
        {
            _spawn = spawn != null 
                ? spawn 
                : throw new ArgumentNullException(nameof(spawn), message: "cannot init objectSpawner by null gameObject.");

            _componentPool   = new();
            _gameObjectPool  = new(
                createFunc:      () => UnityEngine.Object.Instantiate(_spawn),
                actionOnGet:     go =>
                {
                    if (go != null) 
                        go.SetActive(true);
                },
                actionOnRelease: go => go.SetActive(false),
                actionOnDestroy: go => UnityEngine.Object.Destroy(go)
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal TComponent Spawn()
        {
            GameObject obj = _gameObjectPool.Get();

            if (obj == null)
                throw new InvalidOperationException(message: "Pool: get an null object.");

            TComponent component = GetComponentOnPool(key: obj);

            component.DeactiveAction = _gameObjectPool.Release;
            return component;
        }

        private TComponent GetComponentOnPool(in GameObject key)
        {
            if (_componentPool.TryGetValue(key, out TComponent found))
            {
                return found;
            }
            else
            {
                if (key.TryGetComponent(out TComponent component))
                {
                    _componentPool.Add(key, component);
                    return component;
                }
            }
            throw new Exception($"[comp] {typeof(TComponent)} component not found.");
        }
    }
}
