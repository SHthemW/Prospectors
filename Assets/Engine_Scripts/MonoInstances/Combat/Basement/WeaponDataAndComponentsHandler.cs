using Game.Interfaces.GameObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Instances.Combat
{
    public sealed class WeaponDataAndComponentsHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _master;

        private IWeaponAimMaster _aimMaster;
        public IWeaponAimMaster AimMaster
        {
            get
            {
                if ( _aimMaster == null )
                {
                    var master = _master.GetComponent<IWeaponAimMaster>();
                    _aimMaster = master ?? throw new System.NotImplementedException($"[err] {nameof(IWeaponAimMaster)} was not found in {_master.name}");
                }
                return _aimMaster;
            }
        }
    }
}
