using Game.Interfaces;
using Game.Services.Animation;
using Game.Services.Combat;
using Game.Services.Physics;
using Game.Utils.Collections;
using Game.Utils.Extensions;
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
        internal bool OverrideHitActions => _staticData.OverrideHitActions;
        internal float BaseMoveSpeed => _staticData.MoveSpped;
        internal ExecutableAction[] OnHittedActions => _staticData.OnHittedActions;

        [SerializeField]
        private FSMActionData _ai;
        public FSMActionData AI 
            => safe.Checked(_ai);

        [SerializeField]
        private AnimPropertyNameData_SO _animStateNames;
        public IAnimationStateName AnimStateNames
            => _animStateNames.AsSafeInspectorValue(name, a => a != null);

        // components ref
        [Header("Components")]

        [SerializeField]
        private Animator _animator;
        public Animator Animator
            => _animator.AsSafeInspectorValue(name, a => a != null);

        [SerializeField]
        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody =>
            _rigidbody.AsSafeInspectorValue(name, rb => rb != null);

        [SerializeField]
        private Transform _rootTransform;
        internal Transform RootTransform
            => _rootTransform.AsSafeInspectorValue(name, t => t != null);

        [SerializeField]
        private Detector _playerDetector;
        internal Detector PlayerDetector 
            => _playerDetector.AsSafeInspectorValue(name, d => d != null);

        [field: SerializeField]
        internal ObjectComponent<IHoldCharHealth> Health { get; set; }

        public SingletonComponent<Transform> HitEffectParent { get; set; } = new("@HitEffects");
        public SingletonComponent<Transform> HitHoleParent { get; set; } = new("@HitHoles");
    }
}
