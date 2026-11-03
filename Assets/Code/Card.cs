using UnityEngine;

namespace Code
{
    public abstract class Card : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
        [SerializeField] private int prize;
        public int Prize => prize;
    }
}