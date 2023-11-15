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
        private readonly GameObject[] _objects;

        public ObjActiveTurner(GameObject[] objects)
        {
            _objects = objects ?? throw new ArgumentNullException(nameof(objects));
        }
        public void EnableActiveOfObject(int[] index)
        {
            if (index == null || index.Length == 0) 
                throw new ArgumentNullException(nameof(index));

            foreach (int i in index)
                _objects[i].SetActive(true);
        }
        public void EnableActiveOfObjectExclusively(int[] index)
        {
            if (index == null || index.Length == 0)
                throw new ArgumentNullException(nameof(index));

            for (int i = 0; i < _objects.Length; i++)
            {
                if (index.Contains(i))
                    _objects[i].SetActive(true);
                else
                    _objects[i].SetActive(false);
            }
        }

        private ObjActiveTurner()
            => throw new NotImplementedException();
    }
}
