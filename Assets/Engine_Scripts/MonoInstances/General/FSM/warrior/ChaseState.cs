using Game.Interfaces;
using Game.Services.AI;
using UnityEngine;

namespace Game.Instances.General.FSM
{
    internal sealed class ChaseState : AnimationFSMState
    {
        private IHoldAttackTarget _attacker;
        private IHoldCharMovement _charMovement;

        private ChaseStateActionData _actionData;

        private const string FACTOR_NAME = "chase";

        private bool _inAttack;

        protected override sealed void Init(Animator obj)
        {
            base.Init(obj);

            _attacker = GetHolderOnParent<IHoldAttackTarget>(obj);
            _charMovement = GetHolderOnParent<IHoldCharMovement>(obj);

            _actionData = GetHolderOnParent<IHoldAiActionData>(obj).Get<ChaseStateActionData>();
        }

        protected override sealed void EnterStateAction()
        {
            _charMovement.MoveSpeed.AddFactor(() => _inAttack ? 0 : _actionData.SpeedRatioOnChase, FACTOR_NAME);
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
            if (Vector3.Distance(targetPosition, currentPosition) <= _actionData.TurnAttackDistance)
            {
                _attacker.Attack();
                _inAttack = true;
            }
            else
            {
                _inAttack = false;
            }
        }

        protected override sealed void ExitStateAction()
        {
            _charMovement.MoveSpeed.RemoveFactor(FACTOR_NAME);
        }
    }
}
