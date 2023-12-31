using Game.Interfaces;
using UnityEngine;

namespace Game.Instances.General.FSM
{
    internal sealed class ChaseState : AnimationFSMState
    {
        private IHoldAttackTarget _attacker;
        private IHoldCharMovement _charMovement;

        private const string FACTOR_NAME = "chase";

        protected override sealed void Init(Animator obj)
        {
            base.Init(obj);

            _attacker = GetHolderOnParent<IHoldAttackTarget>(obj);
            _charMovement = GetHolderOnParent<IHoldCharMovement>(obj);
        }

        protected override sealed void EnterStateAction()
        {
            _charMovement.MoveSpeed.AddFactor(() => 2, FACTOR_NAME);
        }

        protected override sealed void UpdateStateAction()
        {
            if (_attacker.Target == null)
                return;

            var targetPosition = _attacker.Target.position;
            var currentPosition = _animator.transform.position;

            // chase
            _charMovement.MoveDirection = targetPosition - currentPosition;

            // try attack
            if (Vector3.Distance(targetPosition, currentPosition) <= 2)
            {
                _attacker.Attack();
            }
        }

        protected override sealed void ExitStateAction()
        {
            _charMovement.MoveSpeed.RemoveFactor(FACTOR_NAME);
        }
    }
}
