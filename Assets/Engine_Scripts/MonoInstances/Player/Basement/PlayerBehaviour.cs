using System.Collections;
using UnityEngine;

namespace Game.Instances.Player
{
    internal abstract class PlayerBehaviour : MonoBehaviour
    {
        private PlayerDataHandler _dataHandler;
        protected PlayerDataHandler DataHandler 
        { 
            get 
            { 
                if (_dataHandler == null)
                    _dataHandler = GetComponent<PlayerDataHandler>();
                return _dataHandler; 
            }
        }

        private PlayerComponents _components;
        protected PlayerComponents Components
        {
            get
            {
                if (_components == null)
                    _components = GetComponent<PlayerComponents>();
                return _components;
            }
        }
    }
}