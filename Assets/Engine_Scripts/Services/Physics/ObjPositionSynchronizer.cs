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
        private readonly Transform[] _toBeSync;

        public ObjPositionSynchronizer(Transform[] toBeSync)
        {
            _toBeSync = toBeSync ?? throw new ArgumentNullException(nameof(toBeSync));
        }
        public void SyncTo(Vector3 targetPosition)
        {
            foreach (var t in _toBeSync)
                t.position = targetPosition;
        }
        public void SyncToIfGive(Vector3? targetPosition)
        {
            if (targetPosition == null)
                return;

            SyncTo(targetPosition.Value);
        }

        private ObjPositionSynchronizer()
            => throw new NotImplementedException();
    }
}
