using Game.Interfaces;
using UnityEngine;

namespace Game.Instances.General.FSM
{
    internal sealed class ChaseState : AnimationFSMState
    {
        private IHoldAttackTarget _chase;
        private IHoldCharMovement _charMovement;

        private const string FACTOR_NAME = "chase";

        protected override sealed void Init(Animator obj)
        {
            base.Init(obj);

            _chase = GetHolderOnParent<IHoldAttackTarget>(obj);
            _charMovement = GetHolderOnParent<IHoldCharMovement>(obj); 
        }

        protected override sealed void EnterStateAction()
        {
            _charMovement.MoveSpeed.AddFactor(() => 2, FACTOR_NAME);
        }

        protected override sealed void UpdateStateAction()
        {
            if (_chase.Target == null)
                return;

            _charMovement.MoveDirection = _chase.Target.position - _animator.transform.position;
        }

        protected override sealed void ExitStateAction()
        {
            _charMovement.MoveSpeed.RemoveFactor(FACTOR_NAME);
        }
    }
}
