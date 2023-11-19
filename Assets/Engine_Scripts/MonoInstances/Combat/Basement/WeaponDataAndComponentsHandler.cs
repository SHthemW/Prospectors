﻿using Game.Interfaces;
using Game.Services.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Combat
{
    public sealed class WeaponDataAndComponentsHandler : MonoBehaviour, IWeapon
    {
        [SerializeField]
        private WeaponStaticData_SO _staticData; // TODO: ** impl weapon combat data

        [SerializeField]
        private Transform _grip;

        private IWeaponMaster _master;     

        IWeaponMaster IWeapon.Master 
        { 
            get => _master; 
            set => _master = value; 
        }
        Vector3 IWeapon.GripPosition => _grip.position;

        public Vector3 AimingPosition => _master.AimingPosition;
        public Vector3 MasterPosition => _master.CenterPosition;
        public Vector3 HandlePosition => _master.CurrentHandPositionGetter.Invoke();
    }
}
