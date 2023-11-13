using System.Collections;
using UnityEngine;

namespace Game.Instances.Player
{
    [CreateAssetMenu(fileName="PlayerData", menuName="Data/Player")]
    internal sealed class PlayerStaticData_SO : ScriptableObject
    {
        [SerializeField]
        private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;

        [SerializeField] 
        private float _aimHeight;
        public float AimHeight => _aimHeight;

        [SerializeField]
        private Vector2 _maxFollowOffsetDuringAim;
        public Vector2 MaxFollowOffsetDuringAim => _maxFollowOffsetDuringAim;
    }
}