using System;
using UnityEngine;
using System.Data;

namespace Game.Utils.Collections
{
    [Serializable]
    public struct TaggedItem<TItem>
    {     
        [SerializeField]
        private string _tag;

        [SerializeField]
        private TItem _item;
 
        public readonly string Tag
        {
            get
            {
                if (string.IsNullOrEmpty(_tag))
                    throw new NoNullAllowedException();
                return _tag;
            }
        }
        public readonly TItem Item
        {
            get
            {
                if (_item == null)
                    throw new NoNullAllowedException();
                return _item;
            }
        }
    }
}
