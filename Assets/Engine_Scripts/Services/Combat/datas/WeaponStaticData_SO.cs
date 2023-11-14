using Game.Interfaces;
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
        {
            get
            {
                if (_prefeb == null)
                    throw new NotImplementedException();
                return _prefeb;
            }
        }
        private void CheckPrefeb()
        {
            if (_prefeb == null)
            {        
                Debug.LogWarning($"[data] {name} 的prefeb未配置.");
                return;
            }
            if (!_prefeb.TryGetComponent<IWeapon>(out _))
            {
                Debug.LogWarning($"[data] {name} 的prefeb不是武器预制体.");
                return;
            }
        }

        [Header("Runtime")]

        [SerializeField]
        private int _damage;

        private void OnValidate()
        {
            CheckPrefeb();
        }
    }
}
