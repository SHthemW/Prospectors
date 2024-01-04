using System;
using UnityEngine;

namespace Game.Utils.Extensions
{
    public static class PropertyExtensions
    {
        [Obsolete("use Checker func instead.")]
        public static T AsSafeInspectorValue<T>(this T property, string objName, Func<T, bool> whatIsValid, bool justWarning = false)
        {
            if (!whatIsValid.Invoke(property))
            {
                string msg = 
                    $"{Prefix.DATA}" +
                    $"{Fmt.YELLOW}{objName}{Fmt.RESET} has invalid property." +
                    $"(type {Fmt.YELLOW}{typeof(T)}{Fmt.RESET}) in inspector. " +
                    $"please check.";

                if (justWarning)
                    Debug.LogWarning(msg);
                else
                    throw new Exception(msg);
            }
            return property;
        }
    }
}
