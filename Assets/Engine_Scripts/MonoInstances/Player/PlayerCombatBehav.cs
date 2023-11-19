using Game.Interfaces;
using Game.Services.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerCombatBehav : PlayerBehaviour, IWeaponMaster
    {
        [SerializeField]
        private List<WeaponStaticData_SO> _testWeapons;
        private int _currentTestWeaponIndex;

        private WeaponSwitcher _weaponSwitcher;

        Vector3 IWeaponMaster.CenterPosition => transform.position;
        Vector3 IWeaponMaster.AimingPosition => Components.AimPoint.transform.position;
        IWeapon IWeaponMaster.CurrentWeapon 
        { 
            get => this.DataHandler.CurrentWeapon; 
            set => this.DataHandler.CurrentWeapon = value; 
        }
        Func<Vector3> IWeaponMaster.CurrentHandPositionGetter =>
            () => Components.PlayerModels  .First(i => i.Tag == "Front").Item.activeInHierarchy
                ? Components.CharacterHands.First(i => i.Tag == "Front").Item.position
                : Components.CharacterHands.First(i => i.Tag == "Back") .Item.position;


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
