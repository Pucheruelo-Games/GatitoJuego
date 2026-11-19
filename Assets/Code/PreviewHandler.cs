using UnityEngine;

namespace Code
{
    public class PreviewHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer foreground;
        [SerializeField] private SpriteRenderer background;
        [SerializeField] private Sprite circle;
        [SerializeField] private Sprite square;
        private bool _generated = false;
        private Tile _tile;
        private bool _available;
        

        public void Init(Tile tile, bool available)
        {
            _tile = tile;
            _available = available;
        }

        public void GenerateCatPreview(CatCard cat)
        {
            transform.position = _tile.transform.position + Vector3.up;

            var newColor = new Color();
            
            if (!_available || _tile.Cat != null) newColor = Color.red;
            else newColor = Color.white;

            newColor.a = 0.5f;

            foreground.color = newColor;
            
            if (_generated) return;
            _generated = true;
            foreground.sprite = cat.Preview;
        }

        public void GenerateCirclePreview(Sprite fgSprite, float radius)
        {
            transform.position = _tile.transform.position;
            
            var newColor = new Color();
            
            if (!_available) newColor = Color.red;
            else newColor = Color.blue;

            newColor.a = 0.5f;

            foreground.color = newColor;
            background.color = newColor;
            
            if (_generated) return;
            _generated = true;
            
            foreground.sprite = fgSprite;
            background.sprite = circle;
            background.transform.localScale = Vector3.one * radius;
        }
        
        public void GenerateSquarePreview(Sprite fgSprite, float width, float height)
        {
            transform.position = _tile.transform.position;
            
            var newColor = new Color();
            
            if (!_available) newColor = Color.red;
            else newColor = Color.blue;
            
            newColor.a = 0.5f;

            background.color = newColor;
            foreground.color = newColor;
            
            if (_generated) return;
            _generated = true;
            
            if (fgSprite) foreground.sprite = fgSprite;
            background.sprite = square;
            background.transform.localScale = Vector3.up * height + Vector3.right * width;
        }

        public void ReleasePreview()
        {
            foreground.sprite = null;
            background.sprite = null;
            
            foreground.color = Color.white;
            background.color = Color.white;

            foreground.transform.localScale = Vector3.zero;
            background.transform.localScale = Vector3.zero;
            
            _generated = false;
        }
    }
}