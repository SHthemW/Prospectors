using Game.Utils.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Combat
{
# nullable enable
    [Serializable]
    public sealed class Magazine
    {
        private readonly int _maxCapacity;

        private readonly Action<int>? _actionAfterReload;

        [SerializeField, ReadOnly]
        private int _currentCapacity;

        public Magazine(int maxCapacity, Action<int>? actAfterReload = null)
        {
            _maxCapacity = maxCapacity;
            _actionAfterReload = actAfterReload;
        }
        public bool TryUse(int useNum = 1)
        {
            if (_currentCapacity <= 0)
                return false;

            _currentCapacity -= useNum;
            return true;
        }
        public void Reload(int reloadNum)
        {
            if (reloadNum > _maxCapacity)
                throw new ArgumentException();

            _currentCapacity = reloadNum;
            _actionAfterReload?.Invoke(reloadNum);

        }
    }
# nullable disable
}
