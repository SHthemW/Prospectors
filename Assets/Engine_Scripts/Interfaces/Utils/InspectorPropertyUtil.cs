using Game.Utils;
using System;
using UnityEngine;

namespace Game.Utils
{
    public static class InspectorPropertyUtil
    {
        public static T TryGetIf<T>(this T property, string objName, Func<T, bool> whatIsValid)
        {
            if (!whatIsValid.Invoke(property))
                throw new Exception(
                    $"{Prefix.DATA}" +
                    $"{Fmt.YELLOW}{objName}{Fmt.RESET} has invalid property " +
                    $"(type {Fmt.YELLOW}{typeof(T)}{Fmt.RESET}) in inspector. " +
                    $"please check.");
            return property;
        }
    }
}
