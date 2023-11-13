using Game.Interfaces;
using Game.Services.Combat;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerCombatBehav : PlayerBehaviour, IWeaponMaster
    {
        [SerializeField]
        private WeaponData_SO _testWeapon;

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
                _weaponSwitcher.EquipOrSwitchWeapon(_testWeapon);
            }
        }
    }
}
