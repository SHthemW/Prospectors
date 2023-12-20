using Game.Interfaces;
using Game.Services.Animation;
using Game.Services.Physics;
using Game.Utils.Collections;
using Game.Utils.Extensions;
using UnityEngine;

namespace Game.Instances.Mob
{
    internal sealed class MobDataAndComponentHandler : MonoBehaviour, 
        // identity
        IMob, 
        // holders
        IHoldAnimStateName, IHoldCharRigidbody, IHoldCharAnimator, IHoldCharMoveSpeed, IHoldCharMovement
    {
        // static datas

        [SerializeField]
        private MobStaticData_SO _staticData;
        int IMob.MaxHealth => _staticData.MaxHealth;
        float IHoldCharMoveSpeed.MoveSpeed
            => _staticData.MoveSpped;
        internal float BaseMoveSpeed => _staticData.MoveSpped;
        internal int HitTimesConsumption => _staticData.HitTimesConsumption;
        internal bool OverrideHitActions => _staticData.OverrideHitActions;
        internal ExecutableAction[] OnHittedActions => _staticData.OnHittedActions;

        [SerializeField]
        private MobBrain_SO _brain;
        internal IMobCombatBrain Brain 
            => _brain.AsSafeInspectorValue(name, b => b != null);

        [SerializeField]
        private AnimPropertyNameData_SO _animPropertyName;
        IAnimationStateName IMob.AnimNames
            => _animPropertyName.AsSafeInspectorValue(name, a => a != null);
        IAnimationStateName IHoldAnimStateName.StateName
            => _animPropertyName.AsSafeInspectorValue(name, a => a != null);

        // components ref
        [Header("Components")]

        [SerializeField]
        private Animator _animator;
        Animator IMob.Animator
            => _animator.AsSafeInspectorValue(name, a => a != null);
        Animator IHoldCharAnimator.Animator
            => _animator.AsSafeInspectorValue(name, a => a != null);

        [SerializeField]
        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody =>
            _rigidbody.AsSafeInspectorValue(name, rb => rb != null);

        [SerializeField]
        private Transform _rootTransform;
        internal Transform RootTransform 
            => _rootTransform.AsSafeInspectorValue(name, t => t != null);

        public SingletonComponent<Transform> HitEffectParent { get; set; } = new("@HitEffects");
        public SingletonComponent<Transform> HitHoleParent { get; set; } = new("@HitHoles");


        [field: Header("Datas")]

        [field: SerializeField, Utils.Attributes.ReadOnly]
        public int CurrentHealth { get; set; }

        [field: SerializeField, Utils.Attributes.ReadOnly]
        public Vector3 MoveDirection { get; set; }

        [field: SerializeField, Utils.Attributes.ReadOnly]
        public DynamicData<float> MoveSpeed { get; set; } = new(
            howToMerge: (f1, f2) => f1 * f2,
            factorBase: 1f
            );
    }
}
