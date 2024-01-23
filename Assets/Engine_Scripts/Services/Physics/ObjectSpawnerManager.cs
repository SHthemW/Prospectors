using Game.Interfaces.GameObj;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Combat
{
    public sealed class ObjectSpawnerManager<TComponent> where TComponent : IDestoryManagedObject
    {
        private readonly static Dictionary<GameObject, ObjectSpawner<TComponent>> _pool = new();

        public ObjectSpawnerManager()
        {

        }

        public TComponent Spawn(in GameObject gameObject)
        {
            if (gameObject == null)
                throw new ArgumentNullException("object spawn mgr: cannot spawn null gameObject.");

            TComponent componentOnObj;

            if (_pool.TryGetValue(key: gameObject, out var pool))
            {
                componentOnObj = pool.Spawn();
            }
            else
            {
                Debug.Log($"[Pool] an spawn pool instance created. object: {gameObject.name}");
                _pool.Add(gameObject, new ObjectSpawner<TComponent>(gameObject));
                componentOnObj = _pool[gameObject].Spawn();
            }
            return componentOnObj;
        }
    }
}
