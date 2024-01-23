using Game.Interfaces;
using Game.Interfaces.Data;
using Game.Services.Combat;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobLifeCycleBehav : MobBehaviour, IHoldCharHealth, IEnableOnAliveOnly
    {
        /*
         *  datas
         */

        [field: Header("Datas")]

        [field: SerializeField, Utils.Attributes.ReadOnly]
        public int CurrentHealth { get; set; }

        [field: SerializeField]
        public bool Enable { get; set; } = true;

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
            if (!this.Enable)
                return;

            if (CurrentHealth <= 0)
            {
                foreach (var action in ThisMob.OnDeadActions)
                    action.Execute();

                foreach (var toDisables in GetComponents<IEnableOnAliveOnly>())
                    toDisables.Enable = false;
            }
        }
    }
}
