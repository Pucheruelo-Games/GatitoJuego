using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "Mouse", menuName = "Entity/Mouse", order = 0)]
    public class MouseData : Entity
    {
        [SerializeField] private float speed;
        public float Speed => speed;
        [SerializeField] private int reward;
        public int Reward => reward;
        [SerializeField] private int difficulty;
        public int Difficulty => difficulty;
    }
}