using System;
using UnityEngine;

namespace Game.Utils
{
    public static class InspectorPropertyUtil
    {
        public static T TryGetIf<T>(this T property, string objName, Func<T, bool> whatIsValid)
        {
            if (!whatIsValid.Invoke(property))
                throw new Exception($"[data] {objName} has invalid property (type {typeof(T)}) in inspector. please check.");
            return property;
        }
    }
}
