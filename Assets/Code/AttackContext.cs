using UnityEngine;

namespace Code
{
    public struct AttackContext
    {
        public Vector2 SourcePosition;
        public Vector2 Direction;
        public LayerMask TargetLayerMask;

        public AttackContext(Vector2 sourcePosition, Vector2 direction, LayerMask targetLayerMask)
        {
            SourcePosition = sourcePosition;
            Direction = direction;
            TargetLayerMask = targetLayerMask;
        }
    }
}