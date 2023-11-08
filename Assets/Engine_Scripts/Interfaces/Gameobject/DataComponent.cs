using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces.GameObj
{
    public abstract class DataComponent: MonoBehaviour
    {
        public T Get<T>(string propertyName)
        {
            Debug.Log("call: " +  propertyName);
            var properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == propertyName && property.PropertyType == typeof(T))
                {
                    Debug.Log($"the val is: {(T)property.GetValue(this)}");
                    return (T)property.GetValue(this);
                }
            }
            throw new TypeAccessException($"[data] property {propertyName} with type {typeof(T)} was not found in {GetType().Name}.");
        }
    }

    [Serializable]
    public struct Property<T>
    {
        [SerializeField]
        private DataComponent _dataSource;

        [SerializeField]
        private string _propertyName;

        private T _value;

        public T Value
        {
            get 
            {
                if (_value == null || _value.Equals(default(T)))
                    _value = _dataSource.Get<T>(_propertyName);              
                return _value; 
            }
        }
    }
}
