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
                case (Transform parent, Func<Vector3> position, Func<Quaternion> rotation):

                    Instantiate(
                        original: _spawn,
                        parent:   parent,
                        position: _overridePosition == default ? position() : _overridePosition,
                        rotation: _overrideRotation == default ? rotation() : Quaternion.Euler(_overrideRotation));
                    break;

                // generate on pool
                case (Transform parent, Func<Vector3> position, Func<Quaternion> rotation, ObjectSpawner<IDestoryManagedObject> pool):

                    var obj = pool.Spawn(_spawn);

                    obj.transform.position = _overridePosition == default ? position() : _overridePosition;
                    obj.transform.rotation = _overrideRotation == default ? rotation() : Quaternion.Euler(_overrideRotation);
                    obj.transform.parent   = parent;
                    break;

                // invalid argument
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}