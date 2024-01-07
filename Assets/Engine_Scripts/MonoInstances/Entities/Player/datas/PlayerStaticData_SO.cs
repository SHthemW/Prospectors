using Game.Interfaces;
using UnityEngine;

namespace Game.Instances.Player
{
    [CreateAssetMenu(fileName="PlayerData", menuName="Data/Player")]
    internal sealed class PlayerStaticData_SO : ScriptableObject
    {
        private readonly static Checker safe = new(nameof(PlayerStaticData_SO));

        [SerializeField]
        private float _moveSpeed;
        public float MoveSpeed
            => safe.Checked(_moveSpeed, fatal: false);

        [SerializeField] 
        private float _aimHeight;
        public float AimHeight
            => safe.Checked(_aimHeight);

        [SerializeField]
        private Vector2 _maxFollowOffsetDuringAim;
        public Vector2 MaxFollowOffsetDuringAim 
            => safe.Checked(_maxFollowOffsetDuringAim);
    }
}