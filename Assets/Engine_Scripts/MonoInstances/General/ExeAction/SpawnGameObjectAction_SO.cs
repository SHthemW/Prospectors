using Game.Interfaces;
using Game.Interfaces.GameObj;
using Game.Services.Combat;
using System;
using UnityEngine;
using UnityEngine.Pool;

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

        private bool HasOverrideProperty => 
            _overridePosition != default || _overrideRotation != default;

        protected override sealed bool MustHaveArgument => false;
        protected override sealed void Execute(in object arg = null)
        {
            switch (arg)
            {
                // use default value and generate on world
                case null:
                    Instantiate(
                        original: _spawn,
                        position: _overridePosition,
                        rotation: Quaternion.Euler(_overrideRotation));
                    break;

                // generate on world
                case (Transform parent, Transform caster):
                    if (HasOverrideProperty)
                        Debug.LogWarning($"[action] in {name}, if have any override property, argument will be ignored.");
                    Instantiate(
                        original: _spawn,
                        parent:   parent,
                        position: caster.position,
                        rotation: caster.rotation);
                    break;

                // generate on pool
                case (Transform parent, Transform caster, ObjectSpawner<IDestoryManagedObject> pool):
                    if (HasOverrideProperty)
                        Debug.LogWarning($"[action] in {name}, if have any override property, argument will be ignored.");

                    var obj = pool.Spawn(_spawn);

                    obj.transform.position = caster.position;
                    obj.transform.rotation = caster.rotation;
                    obj.transform.parent   = parent;

                    break;

                // invalid argument
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}