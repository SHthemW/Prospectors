using Game.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Combat
{
    public sealed class WeaponSwitcher
    {
        private readonly Transform     _weaponParent;
        private readonly IWeaponMaster _weaponMaster;

        public WeaponSwitcher(Transform weaponParent, IWeaponMaster weaponMaster)
        {
            _weaponParent = weaponParent != null ? weaponParent : throw new ArgumentNullException(nameof(weaponParent));
            _weaponMaster = weaponMaster ?? throw new ArgumentNullException(nameof(weaponMaster));
        }
        public void EquipOrSwitchWeapon(WeaponStaticData_SO weaponData)
        {
            if (weaponData == null)
                throw new ArgumentNullException(nameof(weaponData));

            if (_weaponParent.childCount != 0)
                RemoveCurrentWeapon();

            EquipNewWeapon(weaponData);
        }

        private WeaponSwitcher() 
            => throw new NotImplementedException();
        private void RemoveCurrentWeapon()
        {
            UnityEngine.Object.Destroy(_weaponParent.GetChild(0).gameObject);
        }
        private void EquipNewWeapon(WeaponStaticData_SO weaponData)
        {
            var weaponObj = UnityEngine.Object.Instantiate(weaponData.Prefeb, _weaponParent);

            if (!weaponObj.TryGetComponent(out IWeapon weapon))
                throw new NotImplementedException();

            // init weapon
            weapon.Master = _weaponMaster;
        }
    }
}
