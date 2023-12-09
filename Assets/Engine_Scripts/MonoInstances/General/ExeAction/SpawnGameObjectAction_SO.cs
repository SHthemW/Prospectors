using Game.Interfaces;
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
                case (Transform parent, Transform caster, Func<ObjectPool<GameObject>> poolGetter, Action<ObjectPool<GameObject>> poolSetter):
                    if (HasOverrideProperty)
                        Debug.LogWarning($"[action] in {name}, if have any override property, argument will be ignored.");

                    var pool = poolGetter.Invoke();

                    if (pool == null) poolSetter.Invoke(new
                    (
                        createFunc:      () => Instantiate(_spawn, parent),
                        actionOnGet:     go => go.SetActive(true),
                        actionOnRelease: go => go.SetActive(false),
                        actionOnDestroy: go => Destroy(go)
                    ));

                    pool?.Get().transform
                        .SetPositionAndRotation(caster.position, caster.rotation);
                    break;

                // invalid argument
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}