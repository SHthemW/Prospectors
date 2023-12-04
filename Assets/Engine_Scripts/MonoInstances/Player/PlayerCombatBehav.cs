using Game.Interfaces;
using Game.Services.Combat;
using System;
using System.Linq;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerCombatBehav : PlayerBehaviour, IWeaponMaster
    {
        private WeaponSwitcher _weaponSwitcher;

        bool IWeaponMaster.WantToShoot 
            => this.DataHandler.PressingShootKey;
        IWeapon IWeaponMaster.CurrentWeapon
        {
            get => this.DataHandler.CurrentWeapon;
            set => this.DataHandler.CurrentWeapon = value;
        }
        Vector3 IWeaponMaster.CenterPosition 
            => transform.position;
        Vector3 IWeaponMaster.AimingPosition 
            => Components.AimPoint.transform.position; 
        Func<Vector3> IWeaponMaster.CurrentHandPositionGetter =>
            () => Components.CharModels.First(i => i.Tag == "Front").Item.activeInHierarchy
                ? Components.CharHands.First(i => i.Tag == "Front").Item.position
                : Components.CharHands.First(i => i.Tag == "Back") .Item.position;
        Animator[] IWeaponMaster.CharacterAnimators => Components.CharAnimators;

        int IWeaponMaster.TryGetBulletFromInventory(int require)
        {
            if (require == 0)
                throw new ArgumentException();

            // inventory number is enough
            if (DataHandler.CurrentInventoryBulletCount >= require)
            {
                DataHandler.CurrentInventoryBulletCount -= require;
                return require;
            }
            // not enough
            else
            {
                int remain = DataHandler.CurrentInventoryBulletCount;
                DataHandler.CurrentInventoryBulletCount = 0;
                return remain;
            }
        }

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
                this.DataHandler.PressingShootKey = true;

            if (Input.GetMouseButtonUp(button: 0))
                this.DataHandler.PressingShootKey = false;
        }
    }
}
