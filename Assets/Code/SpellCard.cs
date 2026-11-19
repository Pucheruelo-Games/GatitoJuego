using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code
{
    [CreateAssetMenu(fileName = "SpellCard", menuName = "Card/SpellCard")]
    public class SpellCard : Card
    {
        [SerializeField] private SpellBehaviour spell;
        public SpellBehaviour Spell => spell;
        [SerializeField] private Sprite foregroundSprite;
        [SerializeField] private PreviewType previewType;
        [SerializeReference] private PreviewConfig previewConfig;
        public override void GeneratePreview(PreviewHandler handler)
        {
            previewConfig?.Generate(handler, foregroundSprite);
        }

        private void OnValidate()
        {
            switch (previewType)
            {
                case PreviewType.Circle: previewConfig = new CirclePreviewConfig(); break;
                case PreviewType.Square: previewConfig = new SquarePreviewConfig(); break;
            }
        }
    }

    public enum PreviewType
    {
        Circle, Square
    }
    
    [Serializable]
    public abstract class PreviewConfig
    {
        public abstract void Generate(PreviewHandler handler, Sprite foregroundSprite);
    }
    
    [Serializable]
    public class CirclePreviewConfig : PreviewConfig
    {
        [SerializeField] private float radius = 1f;

        public override void Generate(PreviewHandler handler, Sprite foregroundSprite)
        {
            handler.GenerateCirclePreview(foregroundSprite, radius); 
        }
    }
    
    [Serializable]
    public class SquarePreviewConfig : PreviewConfig
    {
        [SerializeField] private float width = 1f;
        [SerializeField] private float height = 1f;

        public override void Generate(PreviewHandler handler, Sprite foregroundSprite)
        {
            handler.GenerateSquarePreview(foregroundSprite, width, height); 
        }
    }
}