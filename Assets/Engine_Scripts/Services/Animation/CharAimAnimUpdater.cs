using Game.Interfaces.GameObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Services.Combat 
{ 
    public sealed class CharAimAnimUpdater
    {
        private readonly Transform _aimTarget;
        private readonly Transform _charAimBone;
        private readonly float?    _aimHeight;
        
        public CharAimAnimUpdater(Transform aimTarget, Transform aimBone, float? aimHeight = null)
        {
            _aimTarget   = aimTarget != null ? aimTarget : throw new ArgumentNullException(nameof(aimTarget));
            _charAimBone = aimBone   != null ? aimBone   : throw new ArgumentNullException(nameof(aimBone));
            _aimHeight   = aimHeight;
        }
        public void UpdateAimBone()
        {
            _charAimBone.position = new Vector3(
                _aimTarget.position.x, 
                _aimHeight ?? _aimTarget.position.y, 
                _aimTarget.position.z
                );
        }
    }
}
