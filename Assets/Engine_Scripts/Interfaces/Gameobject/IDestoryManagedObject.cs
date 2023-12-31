﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Interfaces.GameObj
{
    public interface IDestoryManagedObject : IGameObject
    {
        Action<GameObject> DeactiveAction { get; set; }
    }
}
