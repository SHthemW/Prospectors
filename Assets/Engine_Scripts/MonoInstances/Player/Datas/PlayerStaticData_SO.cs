using System.Collections;
using UnityEngine;
using Game.Utils.Extensions;

namespace Game.Instances.Player
{
    [CreateAssetMenu(fileName="PlayerData", menuName="Data/Player")]
    internal sealed class PlayerStaticData_SO : ScriptableObject
    {
        [SerializeField]
        private float _moveSpeed;
        public float MoveSpeed
            => _moveSpeed.AsSafeInspectorValue(name, static p => p > 0);

        [SerializeField] 
        private float _aimHeight;
        public float AimHeight
            => _aimHeight.AsSafeInspectorValue(name, static p => p > 0);

        [SerializeField]
        private Vector2 _maxFollowOffsetDuringAim;
        public Vector2 MaxFollowOffsetDuringAim 
            => _maxFollowOffsetDuringAim.AsSafeInspectorValue(name, p => p != default);
    }
}