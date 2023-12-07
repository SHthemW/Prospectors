using Game.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Instances.General
{
    [CreateAssetMenu(
        fileName = "new SpawnGameObjectAction",
        menuName = "General/ExeAction/SpawnGameObjectAction")]
    internal sealed class SpawnGameObjectAction_SO : ExecutableAction
    {
        [SerializeField] 
        private GameObject _spawn;

        [SerializeField]
        private Vector3 _overridePosition;

        [SerializeField]
        private Vector3 _overrideRotation;

        private bool _hasOverrideProperty => 
            _overridePosition != default || _overrideRotation != default;

        protected override sealed bool MustHaveArgument => false;
        protected override sealed void Execute(in object arg = null)
        {
            if (arg == null || _hasOverrideProperty)
            {
                // use override position
                Instantiate(
                    original: _spawn,
                    position: _overridePosition,
                    rotation: Quaternion.Euler(_overrideRotation));
            }
            else if (arg is (Transform parent, Transform caster))
            {
                if (_hasOverrideProperty)
                    Debug.LogWarning($"[action] in {name}, if {nameof(_overridePosition)} not default, argument will be ignored.");

                Instantiate(
                    original: _spawn, 
                    parent:   parent, 
                    position: caster.position, 
                    rotation: _spawn.transform.rotation);
            }
            else 
                throw new InvalidOperationException();
        }
    }
}