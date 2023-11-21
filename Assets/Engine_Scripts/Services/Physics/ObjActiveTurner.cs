using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Services.Physics
{
    public sealed class ObjActiveTurner
    {
        private readonly Dictionary<string, GameObject> _gameObjects;

        public ObjActiveTurner(TaggedItem<GameObject>[] gameObjects)
        {
            if (gameObjects == null)
                throw new ArgumentNullException(nameof(gameObjects));

            _gameObjects = gameObjects.ToDictionary(o => o.Tag, o => o.Item);
        }
        public void EnableActiveOfObject(string key)
        {
            _gameObjects[key].SetActive(true);
        }
        public void EnableActiveOfObjectExclusively(string key)
        {
            foreach (var obj in _gameObjects)
            {
                if (obj.Key == key)
                    obj.Value.SetActive(true);
                else
                    obj.Value.SetActive(false);
            }
        }

        private ObjActiveTurner()
            => throw new NotImplementedException();
    }
}
