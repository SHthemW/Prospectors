using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IWeapon
    {
        IWeaponMaster Master  { get; set; }
    }
}
