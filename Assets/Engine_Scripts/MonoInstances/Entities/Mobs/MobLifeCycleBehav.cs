using Game.Interfaces;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobLifeCycleBehav : MobBehaviour, IHoldCharHealth
    {
        /*
         *  datas
         */

        [field: Header("Datas")]

        [field: SerializeField, Utils.Attributes.ReadOnly]
        public int CurrentHealth { get; set; }

        /*
         *  behaviours
         */

        private void Start()
        {
            CurrentHealth = ThisMob.MaxHealth;
        }

        private void Update()
        {
            if (CurrentHealth <= 0)
                CombatUtil.Die(ThisMob);
        }
    }
}
