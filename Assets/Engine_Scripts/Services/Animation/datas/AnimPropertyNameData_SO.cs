using Game.Interfaces;
using Game.Services.Combat;
using Game.Utils.Extensions;
using UnityEngine;

namespace Game.Services.Animation
{
    [CreateAssetMenu(fileName="new AnimPropertyNames", menuName="Data/Animation/Property Name")]
    public class AnimPropertyNameData_SO : ScriptableObject, IAnimationStateName
    {
        private readonly static Checker safe = new(nameof(AnimPropertyNameData_SO));

        [SerializeField]
        private string _currentVelocity;
        string IAnimationStateName.CurrentVelocity
            => safe.Checked(_currentVelocity);

        [SerializeField]
        private string[] _onHit;
        string[] IAnimationStateName.OnHit
            => safe.Checked(_onHit);

        [SerializeField]
        private string _idleNotPatrol;
        string IAnimationStateName.IdleNotPatrol 
            => safe.Checked(_idleNotPatrol);

        [SerializeField]
        private string _foundTarget;
        string IAnimationStateName.FoundTarget 
            => safe.Checked(_foundTarget);

        [SerializeField]
        private string _lostTarget;
        string IAnimationStateName.LostTarget 
            => safe.Checked(_lostTarget);

        [SerializeField]
        private string _attack;
        string IAnimationStateName.Attack 
            => safe.Checked(_attack);

        [SerializeField]
        private string _die;
        string IAnimationStateName.Die 
            => safe.Checked(_die);
    }
}