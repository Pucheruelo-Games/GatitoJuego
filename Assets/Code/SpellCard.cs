using UnityEngine;
using UnityEngine.Serialization;

namespace Code
{
    [CreateAssetMenu(fileName = "SpellCard", menuName = "Card/SpellCard")]
    public class SpellCard : Card
    {
        [SerializeField] private SpellBehaviour spell;
        public SpellBehaviour Spell => spell;
        protected override IPreviewable _preview => spell.PreviewBehaviour;
    }
}