using UnityEngine;

namespace Game.Interfaces
{
    public interface IHoldAttackTarget : IDataHolder
    {
        Transform Target { get; set; }
    }
}
