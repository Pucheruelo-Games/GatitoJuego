using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "CatCard", menuName = "Card/CatCard")]
    public class CatCard : Card
    {
        [SerializeField, Min(1)] private uint width = 1;
        public uint Width => width;
        [SerializeField, Min(1)] private uint height = 1;
        public uint Height => height;
        [SerializeField] private CatBehaviour cat;
        public CatBehaviour Cat => cat;
        [SerializeField] private Sprite preview;
        public Sprite Preview => preview;
    }
}