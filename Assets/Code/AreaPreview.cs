using System;
using UnityEngine;

namespace Code
{
    public class AreaPreview : PreviewBehaviour
    {
        [SerializeField] private float width;
        [SerializeField] private float height;
        [SerializeField] private Sprite spellSprite;
        [SerializeField] private Sprite areaSprite;
        [SerializeField] private Color color;

        public override void GeneratePreview(SpriteRenderer previewContainer, Tile tile)
        {
            previewContainer.sprite = spellSprite;
        }

        public override void OnPreviewEnd(SpriteRenderer previewContainer)
        {
            foreach (Transform t in previewContainer.transform)
            {
                if (t == previewContainer.transform) continue;
                Destroy(t.gameObject);
            }
        }
    }
}