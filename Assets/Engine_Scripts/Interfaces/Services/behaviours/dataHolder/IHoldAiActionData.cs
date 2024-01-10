using System;
using System.Reflection;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IHoldAiActionData : IDataHolder
    {
        FSMActionData AIActionProperties { get; }

        TData Get<TData>()
        {
            if (AIActionProperties == null)
                throw new ArgumentNullException();

            Type FSMDataType = AIActionProperties.GetType();

            foreach (var info in FSMDataType.GetProperties())
            {
                if (info.PropertyType == typeof(TData))
                    return (TData)info.GetValue(AIActionProperties);
            }

            foreach (var info in FSMDataType.GetFields())
            {
                if (info.FieldType == typeof(TData))
                    return (TData)info.GetValue(AIActionProperties);
            }

            throw new Exception($"data {typeof(TData)} was not found in type {AIActionProperties.GetType()}.");
        }
    }
}
