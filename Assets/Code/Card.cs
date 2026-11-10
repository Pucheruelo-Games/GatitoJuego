using UnityEngine;

namespace Code
{
    public abstract class Card : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
        [SerializeField] private int cost;
        public int Cost => cost;
        [SerializeField] private float cooldown;
        public float Cooldown => cooldown;
        [SerializeField] private string displayname;
        public string DisplayName => displayname;
    }
}