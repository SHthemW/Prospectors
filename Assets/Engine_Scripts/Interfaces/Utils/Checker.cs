using Game.Utils;
using System;
using UnityEngine;

namespace Game.Interfaces
{
    public readonly struct Checker
    {
        private readonly string _forName;

        public Checker(string forName)
        {
            _forName = forName ?? throw new ArgumentNullException(nameof(forName));
        }

        public T Checked<T>(T orignal, Func<T, bool> valid = null, bool fatal = true)
        {
            valid ??= static p => p switch
            {
                null => false,
                bool => true,
                _ when p is ValueType => !p.Equals(Activator.CreateInstance(p.GetType())),
                _ => true
            };

            if (!valid.Invoke(orignal))
            {
                string msg =
                    $"[data]" +
                    $"{Fmt.YELLOW}{_forName}{Fmt.RESET} has invalid property." +
                    $"(type {Fmt.YELLOW}{typeof(T)}{Fmt.RESET}) in inspector. " +
                    $"please check.";

                if (fatal)
                    throw new Exception(msg);    
                else
                    Debug.LogWarning(msg);
            }
            return orignal;
        }
    }
}
