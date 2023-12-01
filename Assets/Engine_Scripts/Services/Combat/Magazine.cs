using Game.Utils.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Combat
{
    [Serializable]
    public sealed class Magazine
    {
        private readonly int _maxCapacity;

        [SerializeField, ReadOnly]
        private int _currentCapacity;

        public Magazine(int maxCapacity)
        {
            _maxCapacity = maxCapacity;
            _currentCapacity = _maxCapacity;
        }
        public bool TryUse(int useNum = 1)
        {
            if (_currentCapacity <= 0)
                return false;

            _currentCapacity -= useNum;
            return true;
        }
        public void Reload()
        {
            _currentCapacity = _maxCapacity;
        }
    }
}
