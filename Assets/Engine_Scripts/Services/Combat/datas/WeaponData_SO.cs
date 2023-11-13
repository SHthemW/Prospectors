using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Combat
{
    [CreateAssetMenu(fileName = "new Weapon", menuName = "Data/Combat/Weapon")]
    public sealed class WeaponData_SO : ScriptableObject
    {
        [Header("Assets")]

        [SerializeField]
        private GameObject _prefeb;

        [Header("Runtime")]

        [SerializeField]
        private int _damage;

        public GameObject Prefeb 
        { 
            get 
            {
                if (_prefeb == null)
                    throw new NotImplementedException();
                return _prefeb; 
            } 
        }
    }
}
