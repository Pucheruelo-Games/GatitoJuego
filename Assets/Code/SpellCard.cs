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
        [SerializeField] private bool catPreventCast = false;
        public bool CatPreventCast => catPreventCast;
        [SerializeField] private Sprite foregroundSprite;
        private PreviewType _lastPreviewType;
        [SerializeField] private PreviewType previewType;
        [SerializeReference] private PreviewConfig previewConfig;
        public override void GeneratePreview(PreviewHandler handler)
        {
            previewConfig?.Generate(handler, foregroundSprite);
        }

        private void OnValidate()
        {
            if (_lastPreviewType == previewType) return;
            switch (previewType)
            {
                case PreviewType.Circle: previewConfig = new CirclePreviewConfig(); break;
                case PreviewType.Square: previewConfig = new SquarePreviewConfig(); break;
            }
            _lastPreviewType = previewType;
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
        [SerializeField] private int radius = 1;

        public override void Generate(PreviewHandler handler, Sprite foregroundSprite)
        {
            handler.GenerateCirclePreview(foregroundSprite, radius); 
        }
    }
    
    [Serializable]
    public class SquarePreviewConfig : PreviewConfig
    {
        [SerializeField] private int width = 1;
        [SerializeField] private int height = 1;

        public override void Generate(PreviewHandler handler, Sprite foregroundSprite)
        {
            handler.GenerateSquarePreview(foregroundSprite, width, height); 
        }
    }
}