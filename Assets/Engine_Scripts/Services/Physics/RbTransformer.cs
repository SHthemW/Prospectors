using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Services.Physics
{
    public sealed class RbTransformer
    {
        private readonly Rigidbody _rb;
        
        public RbTransformer(Rigidbody rb)
        {
            _rb = rb;
        }
        public void MoveInDirection(Vector3 direction, float speed)
        {
            Vector3 force = direction.normalized * speed;
            _rb.AddForce(force);
        }

        private RbTransformer() { }
    }
}


