using Game.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.General.FSM
{
    internal sealed class TestLogState : AnimationFSMState
    {
        protected override sealed void EnterStateAction()
        {
            if (_animator == null)
                return;

            Debug.Log(_animator.name + "is enter.");
        }

        protected override sealed void UpdateStateAction()
        {
            if (_animator == null)
                return;

            Debug.Log(_animator.name + "is updating..");
        }
    }
}
