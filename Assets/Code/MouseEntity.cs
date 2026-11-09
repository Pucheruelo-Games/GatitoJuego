using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "Mouse", menuName = "Entity/Mouse", order = 0)]
    public class MouseEntity : Entity
    {
        [SerializeField] private float speed;
        public float Speed => speed;
        [SerializeField] private int reward;
        public int Reward => reward;
    }
}