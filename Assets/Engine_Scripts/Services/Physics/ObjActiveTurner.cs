using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Services.Physics
{
    public sealed class ObjActiveTurner
    {
        private readonly GameObject[] _gameObjects;

        public ObjActiveTurner(GameObject gameObject)
        {
            if (gameObject == null)
                throw new ArgumentNullException(nameof(gameObject));
            _gameObjects = new GameObject[1] { gameObject };
        }
        public ObjActiveTurner(GameObject[] gameObjects)
        {
            _gameObjects = gameObjects ?? throw new ArgumentNullException(nameof(gameObjects));
        }

        public void EnableActiveOfObject(int index = 0)
        {
            _gameObjects[index].SetActive(true);
        }
        public void EnableActiveOfObjectExclusively(int index = 0)
        {
            for (int i = 0; i < _gameObjects.Length; i++)
            {
                if (i == index)
                    _gameObjects[i].SetActive(true);
                else
                    _gameObjects[i].SetActive(false);
            }
        }

        private ObjActiveTurner()
            => throw new NotImplementedException();
    }
}
