using UnityEngine;

namespace Code
{
    public abstract class SpellBehaviour : MonoBehaviour
    {
        [SerializeField] private PreviewBehaviour previewBehaviour;
        public PreviewBehaviour PreviewBehaviour => previewBehaviour;
        public abstract void Cast(Tile tile);
    }
}