using Game.Interfaces;
using Game.Services.Animation;
using Game.Services.Combat;
using Game.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerDataHandler : MonoBehaviour
    {
        // movement       
        private float? _moveSpeedBase = null;
        internal float MoveSpeedBase 
        {
            get 
            {
                if (_moveSpeedBase == null)
                    throw new InvalidOperationException("[data] cannot double assign value for a base.");
                return _moveSpeedBase.Value;
            }
            set
            {
                if (_moveSpeedBase != null)
                    throw new InvalidOperationException("[data] cannot double assign value for a base.");
                _moveSpeedBase = value;
            }
        }

        internal event Func<float> MoveSpeedFactors = new(static () => 1);
        internal float CalcCurrentMoveSpeed()
        {
            return MoveSpeedBase * MoveSpeedFactors.Invoke();
        }

        // input
        internal Vector3 CurrentInputDirection => new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        internal bool? CurrentKeyInputDirIsLeft
        {
            get
            {
                return this.CurrentInputDirection.x switch
                {
                    < 0 => true,
                    > 0 => false,
                    _ => null
                };
            }
        }

        // combat
        internal IWeapon CurrentWeapon { get; set; }

        [SerializeField]
        private List<WeaponStaticData_SO> _testWeapons;
        private int _currentTestWeaponIndex = 0;
        internal WeaponStaticData_SO GenerateNextTestWeapon()
        {
            var result = _testWeapons[_currentTestWeaponIndex];
            _currentTestWeaponIndex = _currentTestWeaponIndex == _testWeapons.Count - 1
                ? 0
                : _currentTestWeaponIndex + 1;
            return result;
        }
    }
}