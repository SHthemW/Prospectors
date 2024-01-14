using Game.Utils.Collections;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IHoldCharMovement : IDataHolder
    {
        DynamicData<float> MoveSpeed { get; set; }
        Vector3 MoveDirection { get; set; }
    }
}
