using System;
using System.Reflection;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IHoldAiActionData : IDataHolder
    {
        FSMActionData AI { get; }

        TData Get<TData>()
        {
            if (AI == null)
                throw new ArgumentNullException();

            Type FSMDataType = AI.GetType();

            foreach (var info in FSMDataType.GetProperties())
            {
                if (info.PropertyType == typeof(TData))
                    return (TData)info.GetValue(AI);
            }

            foreach (var info in FSMDataType.GetFields())
            {
                if (info.FieldType == typeof(TData))
                    return (TData)info.GetValue(AI);
            }

            throw new Exception($"data {typeof(TData)} was not found in type {AI.GetType()}.");
        }
    }
}
