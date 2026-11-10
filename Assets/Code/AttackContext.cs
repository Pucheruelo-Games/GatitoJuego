using UnityEngine;

namespace Code
{
    public struct AttackContext
    {
        public Vector2 SourcePosition;
        public Vector2 Direction;
        public LayerMask TargetLayerMask;
        public EntityBehaviour Behaviour;

        public AttackContext(Vector2 sourcePosition, Vector2 direction, LayerMask targetLayerMask, EntityBehaviour entityBehaviour)
        {
            SourcePosition = sourcePosition;
            Direction = direction;
            TargetLayerMask = targetLayerMask;
            Behaviour = entityBehaviour;
        }
    }
}