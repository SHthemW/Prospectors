using Game.Utils.Extensions;
using System.Collections;
using UnityEngine;

namespace Game.Services.Animation
{
    [CreateAssetMenu(fileName="new AnimPropertyNames", menuName="Data/Animation/Property Name")]
    public class AnimPropertyNameData_SO : ScriptableObject
    {
        [SerializeField]
        private string _currentVelocity;
        public string CurrentVelocity 
            => _currentVelocity.AsSafeInspectorValue(name, p => p != default);
    }
}