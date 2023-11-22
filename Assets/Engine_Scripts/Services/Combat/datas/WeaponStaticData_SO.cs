using Game.Interfaces;
using Game.Utils.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Combat
{
    [CreateAssetMenu(fileName = "new Weapon", menuName = "Data/Combat/Weapon")]
    public sealed class WeaponStaticData_SO : ScriptableObject
    {
        [Header("Assets")]

        [SerializeField]
        private GameObject _prefeb;
        public GameObject Prefeb
            => _prefeb.AsSafeInspectorValue(name, p => p != null && _prefeb.TryGetComponent<IWeapon>(out _));

        [Header("Runtime")]

        [SerializeField]
        private int _damage;
    }
}
