using Game.Interfaces;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Instances.General
{
    [CreateAssetMenu(
        fileName = "new SpawnGameObjectAction",
        menuName = "General/ExeAction/SpawnGameObjectAction")]
    internal sealed class SpawnGameObjectAction_SO : ExecutableAction
    {
        [SerializeField] 
        private GameObject _objToSpawn;

        [SerializeField]
        private Vector3 _overridePosition;

        protected override sealed bool MustHaveArgument => false;
        protected override sealed void Execute(in object arg = null)
        {
            if (arg == null || _overridePosition != default)
            {
                // use override position
                Instantiate(
                    original: _objToSpawn,
                    position: _overridePosition,
                    rotation: Quaternion.identity);
            }
            else if (arg is (Transform parent, Vector3 position))
            {
                if (_overridePosition != default)
                    Debug.LogWarning($"[action] in {name}, if {nameof(_overridePosition)} not default, argument will be ignored.");

                Instantiate(
                    original: _objToSpawn, 
                    parent:   parent, 
                    position: position, 
                    rotation: Quaternion.identity);
            }
            else 
                throw new System.InvalidOperationException();
        }
    }
}