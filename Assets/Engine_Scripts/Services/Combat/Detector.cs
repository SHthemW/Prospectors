using Game.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Services.Combat
{
    [RequireComponent(typeof(Collider))]
    public sealed class Detector : MonoBehaviour
    {
        [field: SerializeField, ReadOnly]
        public List<GameObject> InScopes { get; private set; } = new();

        public event Action<Collider> OnCollisionEnter;
        public event Action<Collider> OnCollisionExit;

        [SerializeField]
        private string[] _filterTags;

        private void OnTriggerEnter(Collider other)
        {
            var obj = other.gameObject;

            if (_filterTags.Length > 0 && !_filterTags.Any(tag => obj.CompareTag(tag)))
                return;

            OnCollisionEnter.Invoke(other);

            InScopes.Add(obj);
        }

        private void OnTriggerExit(Collider other)
        {
            OnCollisionExit.Invoke(other);

            InScopes.Remove(other.gameObject);
        }
    }
}
