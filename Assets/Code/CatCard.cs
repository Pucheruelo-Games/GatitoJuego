using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "CatCard", menuName = "Card/CatCard")]
    public class CatCard : Card
    {
        [SerializeField] private CatEntity cat;
        public CatEntity Cat => cat;
    }
}