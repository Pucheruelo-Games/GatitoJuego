using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "ActionCard", menuName = "Card/ActionCard")]
    public class ActionCard : Card
    {
        [SerializeField] private Attack action;
        public Attack Action => action;
    }
}