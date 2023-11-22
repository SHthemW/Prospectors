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

        [SerializeField, ReadOnly]
        private TData? _currValue; // inspector debugging

        private readonly List<Func<TData>> _factorFuncs;
        private readonly Func<TData, TData, TData> _mergeFunc;

        public DynamicData(Func<TData, TData, TData> howToMerge, TData factorBase)
        {
            _wasInited = false;
            _baseValue = default;
            _currValue = default;

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
        public readonly void AddFactor(Func<TData> factorFunc)
        {
            _factorFuncs.Add(factorFunc);
        }

        public TData UpdateCurrentAndGet()
        {
            if (_baseValue == null)
                throw new InvalidOperationException("[data] cannot get dynamic value without init.");

            TData calcResult = _baseValue;

            foreach (var func in _factorFuncs)
            {
                calcResult = _mergeFunc(calcResult, func.Invoke());
            }

            // update current value (because we need it to debug in inspector)
            _currValue = calcResult; 
            // get
            return _currValue;
        }
    }
# nullable disable
}
