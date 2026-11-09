using UnityEngine;
using UnityEngine.Serialization;

namespace Code
{
    public abstract class Entity : ScriptableObject
    {
        [SerializeField] private int health;
        public int Health => health;
        [SerializeField] private float attackSpeed;
        public float AttackSpeed => attackSpeed;
        [SerializeField] private Sprite sprite;
        public Sprite Sprite => sprite;
        [SerializeField] private Attack attack;
        public Attack Attack => attack;
        [SerializeField, Min(1)] private uint width = 1;
        public uint Width => width;
        [SerializeField, Min(1)] private uint height = 1;
        public uint Height => height;
    }
}