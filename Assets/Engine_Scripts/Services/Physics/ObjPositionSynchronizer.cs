using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Services.Physics
{
    public sealed class ObjPositionSynchronizer
    {
        private readonly Transform _syncTarget;
        private readonly Transform[] _toBeSync;

        public ObjPositionSynchronizer(Transform syncTarget, Transform[] toBeSync)
        {
            _syncTarget = syncTarget != null ? syncTarget : throw new ArgumentNullException(nameof(syncTarget));
            _toBeSync = toBeSync ?? throw new ArgumentNullException(nameof(toBeSync));
        }
        public void Sync()
        {
            foreach (var t in _toBeSync)
                t.position = _syncTarget.position;
        }

        private ObjPositionSynchronizer()
            => throw new NotImplementedException();
    }
}
