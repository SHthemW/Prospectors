using Game.Interfaces;
using Game.Interfaces.GameObj;
using Game.Services.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.General
{
    internal sealed class DestoryGameObjectAction : ScriptableAction
    {
        public override dynamic Execute(params dynamic[] args)
        {
            switch (args)
            {
                case dynamic[] a when a.Length == 1 && a[0] is GameObject obj:
                    Destroy(obj);
                    break;

                case dynamic[] a when a.Length == 2 && a[0] is IDestoryManagedObject obj:
                    if (obj.DeactiveAction == null)
                        throw new InvalidOperationException();
                    obj.DeactiveAction.Invoke(obj.gameObject);
                    break;

                default: 
                    throw new InvalidOperationException();
            }
            return null;
        }
    }
}
