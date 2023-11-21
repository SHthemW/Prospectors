using System.Collections;
using UnityEngine;
using static Game.Utils.InspectorPropertyUtil;

namespace Game.Instances.Player
{
    [CreateAssetMenu(fileName="PlayerData", menuName="Data/Player")]
    internal sealed class PlayerStaticData_SO : ScriptableObject
    {
        [SerializeField]
        private float _moveSpeed;
        public float MoveSpeed
            => _moveSpeed.TryGetIf(name, static p => p > 0);

        [SerializeField] 
        private float _aimHeight;
        public float AimHeight
            => _aimHeight.TryGetIf(name, static p => p > 0);

        [SerializeField]
        private Vector2 _maxFollowOffsetDuringAim;
        public Vector2 MaxFollowOffsetDuringAim 
            => _maxFollowOffsetDuringAim.TryGetIf(name, p => p != default);
    }
}