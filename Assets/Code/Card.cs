using UnityEngine;

namespace Code
{
    public abstract class Card : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
    }
}