using UnityEngine;

namespace Code
{
    public interface IPreviewable
    {
        public void GeneratePreview(SpriteRenderer previewContainer, Tile tile);

        public void OnPreviewEnd(SpriteRenderer previewContainer);
    }
}