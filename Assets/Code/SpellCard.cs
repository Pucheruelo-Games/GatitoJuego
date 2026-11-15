using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "SpellCard", menuName = "Card/SpellCard")]
    public class SpellCard : Card
    {
        [SerializeField] private Attack action;
        public Attack Action => action;
    }
}