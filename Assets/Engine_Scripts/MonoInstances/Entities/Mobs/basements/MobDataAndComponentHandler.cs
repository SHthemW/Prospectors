using Game.Interfaces;
using Game.Services.Animation;
using Game.Services.Combat;
using Game.Services.FSM;
using Game.Services.Physics;
using Game.Utils.Collections;
using Game.Utils.Extensions;
using System;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobDataAndComponentHandler : MonoBehaviour, IMob, IHoldCharAnimator, IHoldAnimStateName, IHoldAiActionData
    {
        private static readonly Checker safe = new(nameof(MobDataAndComponentHandler));

        // static datas

        [SerializeField]
        private MobStaticData_SO _staticData;
        internal int MaxHealth => _staticData.MaxHealth;
        internal int HitTimesConsumption => _staticData.HitTimesConsumption;
        internal float BaseMoveSpeed => _staticData.MoveSpped;
        

        [SerializeField]
        private FSMActionData _aiActionProperties;
        public FSMActionData AIActionProperties 
            => safe.Checked(_aiActionProperties);

        [SerializeField]
        private AnimPropertyNameData_SO _animStateNames;
        public IAnimationStateName AnimStateNames
            => safe.Checked(_animStateNames);

        [Header("Behaviours")]

        [SerializeField] 
        private ExecutableAction[] _onHittedActions;
        internal ExecutableAction[] OnHittedActions => safe.Checked(_onHittedActions);
        internal bool OverrideHitActions => OnHittedActions.Length > 0;

        [SerializeField]
        private ExecutableAction[] _onDeadActions;
        internal ExecutableAction[] OnDeadActions => safe.Checked(_onDeadActions);

        [Header("Components")]

        [SerializeField]
        private Animator _animator;
        public Animator Animator
            => safe.Checked(_animator);

        [SerializeField]
        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody =>
            safe.Checked(_rigidbody);

        [SerializeField]
        private Transform _rootTransform;
        internal Transform RootTransform
            => safe.Checked(_rootTransform);

        [SerializeField]
        private Detector _playerDetector;
        internal Detector PlayerDetector 
            => safe.Checked(_playerDetector);

        [field: SerializeField]
        internal ObjectComponent<IHoldCharHealth> Health { get; set; }

        public SingletonComponent<Transform> HitEffectParent { get; set; } = new("@HitEffects");
        public SingletonComponent<Transform> HitHoleParent { get; set; } = new("@HitHoles");
    }
}
