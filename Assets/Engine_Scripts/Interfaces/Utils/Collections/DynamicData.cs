using Game.Utils.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils.Collections
{
# nullable enable
    [Serializable]
    public struct DynamicData<TData>
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

        private readonly List<Func<TData>> _factorFuncs;
        private readonly Func<TData, TData, TData> _mergeFunc;

        public DynamicData(Func<TData, TData, TData> howToMerge, TData factorBase)
        {
            _wasInited = false;
            _baseValue = default;
            _currentValue = default;

            _factorList = new();
            _mergeFunc = howToMerge;
            _factorFuncs = new()
            {
                () => factorBase
            };
        }
        public void Init(TData baseValue)
        {
            if (_wasInited)
                throw new InvalidOperationException("[data] cannot assign to basevalue for twice.");

            this._baseValue = baseValue;
            this._wasInited = true;
        }
        public readonly void AddFactor(Func<TData> factorFunc, string factorName = "unnamed factor")
        {
            _factorFuncs.Add(factorFunc);
            _factorList.Add(factorName);
        }

        public TData UpdateCurrentAndGet()
        {
            if (_baseValue == null)
                throw new InvalidOperationException("[data] cannot get dynamic value without init.");

            TData calcResult = _baseValue;

            foreach (var func in _factorFuncs)
            {
                var effection = func.Invoke();
                
                calcResult = _mergeFunc(calcResult, effection);
            }

            // update current value (because we need it to debug in inspector)
            _currentValue = calcResult; 
            // get
            return _currentValue;
        }
    }
# nullable disable
}
