using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
    public class Card : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
    }
}


