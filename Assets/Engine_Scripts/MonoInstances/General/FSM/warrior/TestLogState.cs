using Game.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances.General.FSM
{
    internal sealed class TestLogState : AnimationFSMState
    {
        [SerializeField]
        private bool _enableStateEnterLog;

        [SerializeField]
        private bool _enableStateUpdateLog;

        [SerializeField]
        private bool _enableStateExitLog;

        protected override sealed void EnterStateAction()
        {
            if (_enableStateEnterLog)
                Debug.Log(_animator.name + "is enter.");
        }

        protected override sealed void UpdateStateAction()
        {
            if (_enableStateUpdateLog)
                Debug.Log(_animator.name + "is updating..");
        }

        protected override sealed void ExitStateAction()
        {
            if (_enableStateExitLog)
                Debug.Log(_animator.name + "is exited..");
        }
    }
}
