using Game.Interfaces.Data;
using Game.Interfaces.GameObj;
using Game.Services.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.Combat
{
    [RequireComponent(typeof(BulletDataAndComponentHandler))]
    internal abstract class BulletBehaviour : MonoBehaviour
    {
        private BulletDataAndComponentHandler _thisBullet;
        protected BulletDataAndComponentHandler ThisBullet 
        { 
            get 
            { 
                if (_thisBullet == null)
                    _thisBullet = GetComponent<BulletDataAndComponentHandler>();
                return _thisBullet; 
            } 
        }
    }
}
