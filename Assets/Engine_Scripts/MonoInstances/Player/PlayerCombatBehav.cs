using Game.Interfaces;
using Game.Services.Combat;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerCombatBehav : PlayerBehaviour, IWeaponMaster
    {
        [SerializeField]
        private List<WeaponData_SO> _testWeapons;
        private int _currentTestWeaponIndex;

        private WeaponSwitcher _weaponSwitcher;

        Vector3 IWeaponMaster.AimingPosition => Components.AimPoint.transform.position;

        private void Awake()
        {
            Components.AimPoint.Init(Components.RootTransform, DataHandler.MaxFollowOffsetDuringAim);

            _weaponSwitcher = new(
                weaponParent: Components.WeaponParent,
                weaponMaster: this
            );
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _weaponSwitcher.EquipOrSwitchWeapon(_testWeapons[_currentTestWeaponIndex]);
                _currentTestWeaponIndex = _currentTestWeaponIndex == _testWeapons.Count - 1
                    ? 0 
                    : _currentTestWeaponIndex + 1;
            }
        }
    }
}
