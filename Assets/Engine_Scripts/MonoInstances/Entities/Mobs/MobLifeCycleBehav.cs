using Game.Interfaces;
using Game.Interfaces.Data;
using Game.Services.Combat;
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

        private void Awake()
        {
            ThisMob.Health = new(this);
        }

        private void Start()
        {
            CurrentHealth = ThisMob.MaxHealth;
        }

        private void Update()
        {
            if (CurrentHealth <= 0)
            {
                foreach (var toDisables in GetComponents<IEnableOnAliveOnly>())
                    toDisables.Enable = false;

                foreach (var action in ThisMob.OnDeadActions)
                    action.Execute();
            }
        }
    }
}
