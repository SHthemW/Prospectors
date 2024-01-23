using Game.Interfaces;
using Game.Interfaces.Data;
using Game.Interfaces.GameObj;
using Game.Services.Animation;
using Game.Services.Combat;
using Game.Services.SAction;
using Game.Utils.Collections;
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
        private ParameterizedAction[] _onHittedActions;
        internal ParameterizedAction[] OnHittedActions => safe.Checked(_onHittedActions);
        internal bool OverrideHitActions => OnHittedActions.Length > 0;

        [SerializeField]
        private ParameterizedAction[] _onDeadActions;
        internal ParameterizedAction[] OnDeadActions => safe.Checked(_onDeadActions);

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

        internal BufferedComponent<IHoldCharHealth> Health { get; set; }
        internal BufferedComponent<IHoldCharHitPosition> HitPosHolder { get; set; }

        public SingletonComponent<Transform> HitEffectParent { get; set; } = new("@HitEffects");
        public SingletonComponent<Transform> HitHoleParent { get; set; } = new("@HitHoles");


        private void Start()
        {
            IExecutableAction.BatchInit(kwargs: new()
            {
                [SActionDataTag.RootGameObject]  = this.RootTransform.gameObject,

                [SActionDataTag.PrimaryAnimator] = this.Animator,

                [SActionDataTag.HitEffectSpawnInfo] = (
                parent:   this.HitEffectParent.Get(),
                position: (Func<Vector3>)   (() => this.HitPosHolder.Get().CurrentHittedPosition),
                rotation: (Func<Quaternion>)(() => transform.rotation),
                pool:     new ObjectSpawner<IDestoryManagedObject>()
                ),
                [SActionDataTag.HitHoleSpawnInfo] = (
                parent:   this.HitHoleParent.Get(),
                position: (Func<Vector3>)   (() => this.HitPosHolder.Get().CurrentHittedPosition),
                rotation: (Func<Quaternion>)(() => transform.rotation),
                pool:     new ObjectSpawner<IDestoryManagedObject>()
                )
            },
            this.OnHittedActions,
            this.OnDeadActions);
        }
    }
}
