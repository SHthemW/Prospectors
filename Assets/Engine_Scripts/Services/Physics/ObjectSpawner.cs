using Game.Interfaces;
using Game.Interfaces.GameObj;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Services.Combat
{
    public sealed class ObjectSpawner<TComponent> where TComponent : IDestoryManagedObject
    {
        private readonly ObjectPool<GameObject>             _gameObjectPool;
        private readonly Dictionary<GameObject, TComponent> _componentPool;

        private GameObject _currentGameObject;

        public ObjectSpawner()
        {
            _componentPool   = new();
            _gameObjectPool  = new(
                createFunc:      () => UnityEngine.Object.Instantiate(_currentGameObject),
                actionOnGet:     go => go.SetActive(true),
                actionOnRelease: go => go.SetActive(false),
                actionOnDestroy: go => UnityEngine.Object.Destroy(go)
            );
        }
        public TComponent Spawn(in GameObject objToSpawn)
        {
            if (objToSpawn == null) 
                throw new ArgumentNullException();

            _currentGameObject = objToSpawn;
            GameObject obj = _gameObjectPool.Get();

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
