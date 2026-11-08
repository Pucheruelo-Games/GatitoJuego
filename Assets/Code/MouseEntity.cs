using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "Mouse", menuName = "Entity/Mouse", order = 0)]
    public class MouseEntity : Entity
    {
        [SerializeField] private float speed;
        public float Speed => speed;
        [SerializeField] private uint reward;
        public uint Reward => reward;
    }
}