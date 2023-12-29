using Game.Interfaces;
using Game.Utils.Extensions;
using UnityEngine;

namespace Game.Services.Animation
{
    [CreateAssetMenu(fileName="new AnimPropertyNames", menuName="Data/Animation/Property Name")]
    public class AnimPropertyNameData_SO : ScriptableObject, IAnimationStateName
    {
        [SerializeField]
        private string _currentVelocity;
        string IAnimationStateName.CurrentVelocity
            => _currentVelocity.AsSafeInspectorValue(name, p => p != default);

        [SerializeField]
        private string[] _onHit;
        string[] IAnimationStateName.OnHit
            => _onHit.AsSafeInspectorValue(name, p => p != default);

        [SerializeField]
        private string _idleNotPatrol;
        string IAnimationStateName.IdleNotPatrol 
            => _idleNotPatrol.AsSafeInspectorValue(name, p => p != default);

        [SerializeField]
        private string _foundTarget;
        string IAnimationStateName.FoundTarget 
            => _foundTarget.AsSafeInspectorValue(name, p => p != default);

        [SerializeField]
        private string _lostTarget;
        string IAnimationStateName.LostTarget 
            => _lostTarget.AsSafeInspectorValue(name, s => s != default);
    }
}