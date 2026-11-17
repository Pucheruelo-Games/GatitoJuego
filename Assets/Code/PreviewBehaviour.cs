using UnityEngine;

namespace Code
{
    public abstract class PreviewBehaviour : MonoBehaviour, IPreviewable
    {
        public abstract void GeneratePreview(SpriteRenderer previewContainer, Tile tile);
        
        public abstract void OnPreviewEnd(SpriteRenderer previewContainer);
        
    }
}