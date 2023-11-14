using Game.Services.Animation;
using System.Collections;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerDataHandler : MonoBehaviour
    {
        [SerializeField]
        private PlayerStaticData_SO _staticData;

        [SerializeField]
        private AnimPropertyNameData_SO _animPropertyNames;
        internal AnimPropertyNameData_SO AnimPropNames => _animPropertyNames;

        internal float CurrentMoveSpeed => _staticData.MoveSpeed;
        internal float AimHeight => _staticData.AimHeight;
        internal Vector2 MaxFollowOffsetDuringAim => _staticData.MaxFollowOffsetDuringAim;


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
        
    }
}