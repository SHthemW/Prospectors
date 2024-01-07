using UnityEngine;

namespace Game.Instances.Mob
{
    [RequireComponent(typeof(Rigidbody))]
    internal abstract class MobBehaviour : MonoBehaviour
    {
        private MobDataAndComponentHandler _thisMob;
        protected MobDataAndComponentHandler ThisMob
        {
            get
            {
                if (_thisMob == null)
                    _thisMob = GetComponent<MobDataAndComponentHandler>();
                return _thisMob;
            }
        }
    }
}
