using Game.Services.Animation;
using Game.Services.Combat;
using System.Collections;
using UnityEngine;

namespace Game.Instances.Player
{
    internal sealed class PlayerViewBehav : PlayerBehaviour
    {
        private CharMovementAnimUpdater _moveAnimUpdater;
        
        private void Awake()
        {
            _moveAnimUpdater = new(Components.CharAnimator, DataHandler.AnimPropNames);          
        }

        private void Update()
        {
            _moveAnimUpdater.UpdateAnim(Components.PlayerRb.velocity.magnitude);
        }
    }
}