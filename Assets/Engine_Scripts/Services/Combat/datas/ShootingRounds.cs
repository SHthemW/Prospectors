using Game.Interfaces;
using Game.Utils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Combat
{
    /// <summary>
    /// a shooting round contains one or more shooting unit.<br/>
    /// one <see cref="ShootingRound"/> means one shoot (one time trigger press). <br/>
    /// </summary>
    [Serializable]
    public struct ShootingRound : IEnumerable<ShootingUnit>
    {
        [SerializeField]
        private ShootingUnit[] _units;

        [Header("Action")]

        [SerializeField]
        private ParameterizedAction[] _gunActions;
        public readonly ParameterizedAction[] GunActions
            => _gunActions;

        [SerializeField]
        private ParameterizedAction[] _masterActions;
        public readonly ParameterizedAction[] MasterActions
            => _masterActions;

        public readonly IEnumerator<ShootingUnit> GetEnumerator()
        {
            return ((IEnumerable<ShootingUnit>)_units).GetEnumerator();
        }
        readonly IEnumerator IEnumerable.GetEnumerator()
        {
            return _units.GetEnumerator();
        }

        public static ShootingRound GetCurrentRound(ref int currentIndex, in ShootingRound[] shootingRounds)
        {
            if (currentIndex >= shootingRounds.Length)
                currentIndex = 0;

            var currentRound = shootingRounds[currentIndex];
            currentIndex++;

            return currentRound;
        }
    }

    /// <summary>
    /// a shooting unit is the smallest unit of bullet shooting.<br/>
    /// it should contains ONLY ONE bullet in each unit.
    /// </summary>
    [Serializable]
    public struct ShootingUnit
    {
        private readonly static Checker safe = new(nameof(ShootingUnit));

        [SerializeField]
        private GameObject _bullet;
        public readonly GameObject Bullet
            => safe.Checked(_bullet, valid: static b => b != null);

        [Header("Action")]

        [SerializeField]
        private ParameterizedAction[] _gunActions;
        public readonly ParameterizedAction[] GunActions
            => _gunActions;

        [SerializeField]
        private ParameterizedAction[] _masterActions;
        public readonly ParameterizedAction[] MasterActions
            => _masterActions;

        [Header("Property")]

        [SerializeField, Tooltip("子弹实际发射出的角度与瞄准角度的偏移量. 默认为0.")]
        private float _shootingAngleOffset;
        public readonly float ShootingAngleOffset
            => _shootingAngleOffset;

        [SerializeField, Tooltip("该子弹的射击延迟. 默认为0.")]
        private float _shootingDelaySecond;
        public readonly float ShootingDelaySecond
            => _shootingDelaySecond;
    }
}
