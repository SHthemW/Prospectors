using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Combat
{
    [CreateAssetMenu(fileName = "new Weapon", menuName = "Data/Combat/Weapon")]
    public sealed class WeaponData_SO : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefeb { get; private set; }
    }
}
