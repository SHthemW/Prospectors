using Game.Interfaces;
using Game.Services.AI;
using UnityEngine;

namespace Game.Instances.General.FSM
{
    internal sealed class PatrolState : AnimationFSMState
    {
        private float _patrolTime;

        private const string MOVE_FACTOR_NAME = "patrol";

        private IHoldCharMovement _charMovement;

        private PatrolStateActionData _actionData;

        protected override sealed void Init(Animator obj)
        {
            base.Init(obj);

            _charMovement = GetHolderOnParent<IHoldCharMovement>(obj);

            _actionData = GetHolderOnParent<IHoldAiActionData>(obj).Get<PatrolStateActionData>();
        }

        protected override sealed void EnterStateAction()
        {
            _patrolTime = UnityEngine.Random.Range(_actionData.MinPatrolTime, _actionData.MaxPatrolTime);

            _charMovement.MoveSpeed.AddFactor(() => _actionData.SpeedRatioOnMove, MOVE_FACTOR_NAME);

            _charMovement.MoveDirection = new Vector3(
                UnityEngine.Random.Range(-1f, 1f),
                0,
                UnityEngine.Random.Range(-1f, 1f)
                ).normalized;
        }

        protected override sealed void UpdateStateAction()
        {
            _patrolTime -= Time.deltaTime;
            if (_patrolTime < 0)
                _animator.SetBool(_stateName.IdleNotPatrol, true);
        }

        protected override sealed void ExitStateAction()
        {
            _charMovement.MoveSpeed.RemoveFactor(MOVE_FACTOR_NAME);
        }
    }
}
