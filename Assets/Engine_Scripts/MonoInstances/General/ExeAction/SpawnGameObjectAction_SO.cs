using Game.Interfaces.GameObj;
using Game.Interfaces.SAction;
using Game.Services.Combat;
using Game.Utils.Extensions;
using System;
using UnityEngine;

namespace Game.Instances.General
{
    [CreateAssetMenu(
        fileName = "new SpawnGameObjectAction",
        menuName = "General/ExeAction/SpawnGameObjectAction")]
    internal sealed class SpawnGameObjectAction_SO : ScriptableAction
    {
        private GameObject _spawn;
        private Vector3    _overridePosition = Vector3.zero;
        private Vector3    _overrideRotation = Vector3.zero;

        /// <param name="objArgs">
        /// <br/> 0. object to spawn: GameObject
        /// </param>
        /// <param name="strArgs">
        /// <br/> [0. override spawn position: float, float, float]
        /// <br/> [1. override spawn rotation: float, float, float]
        /// </param>
        /// <exception cref="ArgumentException"></exception>
        public override void SetStaticArgs(in UnityEngine.Object[] objArgs, in string[] strArgs)
        {
            switch (objArgs)
            {
                case UnityEngine.Object[] oa:
                    try
                    {
                        if (oa[0] is GameObject obj)
                            _spawn = obj;
                        else 
                            throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException) { throw new ArgumentException(); }
                    break;

                default:
                    throw new ArgumentException();
            }

            switch (strArgs)
            {
                case string[] sa when sa.Length == 0:
                    break; 

                case string[] sa when sa.Length > 0:
                    try
                    {
                        _overridePosition = sa[0].ToVector3();
                        _overrideRotation = sa[1].ToVector3();
                    }
                    catch (IndexOutOfRangeException) { break; }
                    break;   

                default:
                    throw new ArgumentException();
            }
        }

        protected override sealed void ExecuteFor(in object runtimeArgs)
        {
            switch (runtimeArgs)
            {
                // use default value and generate on world
                case null:
                    if (_spawn == null)
                        throw new ArgumentException("spawn object is requirement.");
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