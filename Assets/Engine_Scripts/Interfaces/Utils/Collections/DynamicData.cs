using Game.Utils.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils.Collections
{
# nullable enable
    [Serializable]
    public sealed class DynamicData<TData>
    {
        [SerializeField, ReadOnly]
        private TData? _baseValue;
        private bool   _wasInited;

        #region Inspector Debugging
        [SerializeField, ReadOnly]
        private TData? _currentValue;

        [SerializeField, ReadOnly]
        private List<string> _factorList;
        #endregion

        private readonly Dictionary<string, Func<TData>> _factors;
        private readonly Func<TData, TData, TData> _mergeFunc;

        public DynamicData(Func<TData, TData, TData> howToMerge, TData factorBase)
        {
            _wasInited = false;
            _baseValue = default;
            _currentValue = default;

            _factorList = new();
            _mergeFunc = howToMerge;
            _factors = new()
            {
                { "base", () => factorBase }
            };
        }

        public void Init(TData baseValue)
        {
            if (_wasInited)
                throw new InvalidOperationException("[data] cannot assign to basevalue for twice.");

            this._baseValue = baseValue;
            this._wasInited = true;
        }

        public void AddFactor(Func<TData> factorFunc, string factorName)
        {
            _factors.Add(factorName, factorFunc);
            _factorList.Add(factorName);
        }

        public void RemoveFactor(string factorName)
        {
            _factors.Remove(factorName);
            _factorList.Remove(factorName);
        }

        public TData UpdateCurrentAndGet()
        {
            if (_baseValue == null)
                throw new InvalidOperationException("[data] cannot get dynamic value without init.");

            TData calcResult = _baseValue;

            foreach (var func in _factors)
                calcResult = _mergeFunc(calcResult, func.Value.Invoke());

            // update current value (because we need it to debug in inspector)
            _currentValue = calcResult; 
            // get
            return _currentValue;
        }
    }
# nullable disable
}
