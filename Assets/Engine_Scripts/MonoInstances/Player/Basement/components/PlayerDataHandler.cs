using Game.Services.Animation;
using System.Collections;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerDataHandler : MonoBehaviour
    {
        [SerializeField]
        private PlayerData_SO _data;

        [SerializeField]
        private AnimPropertyNameData_SO _animPropertyNames;
        internal AnimPropertyNameData_SO AnimPropNames => _animPropertyNames;

        internal float CurrentMoveSpeed => _data.MoveSpeed;
        internal float AimHeight => _data.AimHeight;
        internal Vector2 MaxFollowOffsetDuringAim => _data.MaxFollowOffsetDuringAim;


        internal Vector3 CurrentInputDirection => new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        internal bool? CurrentFacingDirIsLeft => this.CurrentInputDirection.x switch
        {
            < 0 => true,
            > 0 => false,
            _ => null
        };      
        
    }
}