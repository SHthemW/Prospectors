using Game.Interfaces;
using Game.Services.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerCombatBehav : PlayerBehaviour, IWeaponMaster
    {
        private WeaponSwitcher _weaponSwitcher;

        Vector3 IWeaponMaster.CenterPosition => transform.position;
        Vector3 IWeaponMaster.AimingPosition => Components.AimPoint.transform.position;
        IWeapon IWeaponMaster.CurrentWeapon
        { 
            get => this.DataHandler.CurrentWeapon; 
            set => this.DataHandler.CurrentWeapon = value; 
        }
        Func<Vector3> IWeaponMaster.CurrentHandPositionGetter =>
            () => Components.CharModels.First(i => i.Tag == "Front").Item.activeInHierarchy
                ? Components.CharHands.First(i => i.Tag == "Front").Item.position
                : Components.CharHands.First(i => i.Tag == "Back") .Item.position;


        private void Awake()
        {
            Components.AimPoint.Init(Components.RootTransform, Components.StaticBasicData.MaxFollowOffsetDuringAim);

            _weaponSwitcher = new(
                weaponParent: Components.WeaponParent,
                weaponMaster: this
            );
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _weaponSwitcher.EquipOrSwitchWeapon(DataHandler.GenerateNextTestWeapon());
            }

            if (Input.GetMouseButtonDown(button: 0))
            {
                if ((this as IWeaponMaster).CurrentWeapon == null)
                    return;
                (this as IWeaponMaster).CurrentWeapon.ShootBullet();
            }
        }
    }
}
