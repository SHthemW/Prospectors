using Game.Interfaces.GameObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Services.Combat 
{ 
    public sealed class CharAimTurnUpdater
    {
        private readonly IDynamicPoint _aimPoint;
        private readonly Transform _aimBone;
        private readonly float? _aimHeight;

        public CharAimTurnUpdater(IDynamicPoint aimPoint, Transform aimBone, float? aimHeight = null)
        {
            _aimPoint = aimPoint ?? throw new ArgumentNullException(nameof(aimPoint));
            _aimBone = aimBone != null ? aimBone : throw new ArgumentNullException(nameof(aimBone));
            _aimHeight = aimHeight;
        }
        public void UpdateAimTurn()
        {
            _aimBone.position = new Vector3(
                _aimPoint.Position.x, 
                _aimHeight ?? _aimPoint.Position.y, 
                _aimPoint.Position.z
                );
        }
    }
}
