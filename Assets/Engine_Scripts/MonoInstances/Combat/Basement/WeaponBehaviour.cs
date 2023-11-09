using Game.Interfaces.GameObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Instances.Combat
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        private WeaponDataAndComponentsHandler _thisWeapon;
        protected WeaponDataAndComponentsHandler ThisWeapon 
        { 
            get 
            { 
                if (_thisWeapon == null)
                    _thisWeapon = GetComponent<WeaponDataAndComponentsHandler>();
                return _thisWeapon; 
            } 
        }
    }
}
