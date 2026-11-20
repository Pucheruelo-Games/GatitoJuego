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
        [SerializeField, Min(1)] private uint width = 1;
        public uint Width => width;
        [SerializeField, Min(1)] private uint height = 1;
        public uint Height => height;
        public abstract void GeneratePreview(PreviewHandler handler);
    }
}