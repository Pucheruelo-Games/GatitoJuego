using UnityEngine;

namespace Code
{
    public class PreviewHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer foreground;
        [SerializeField] private SpriteRenderer background;
        [SerializeField] private Color areaPreviewColor;
        [SerializeField] private Sprite[] circles;
        [SerializeField] private Sprite square;
        private bool _generated = false;
        private Tile[] _tiles;
        private Vector2 _center;
        private bool _available;
        

        public void Init(bool available, params Tile[] tiles)
        {
            _tiles = tiles;
            _center = Vector2.zero;
            foreach (var tile in _tiles)
            {
                _center += (Vector2)tile.transform.position;
            }
            _center /= _tiles.Length;
            _available = available;
        }

        public void GenerateCatPreview(CatCard cat)
        {
            transform.position = _center + Vector2.up * 0.2f;

            var newColor = new Color();
            
            if (!_available) newColor = Color.red;
            else newColor = Color.white;

            newColor.a = 0.5f;

            foreground.color = newColor;
            
            if (_generated) return;
            _generated = true;
            foreground.sprite = cat.Preview;
        }

        public void GenerateCirclePreview(Sprite fgSprite, int radius)
        {
            transform.position = _center;

            foreground.color = new Color(1f, 1f, 1f, 0.5f);
            background.color = areaPreviewColor;
            
            if (_generated) return;
            _generated = true;
            
            foreground.sprite = fgSprite;
            background.sprite = circles[radius-1];
        }
        
        public void GenerateSquarePreview(Sprite fgSprite, int width, int height)
        {
            transform.position = _center;

            foreground.color = new Color(1f, 1f, 1f, 0.5f);
            background.color = areaPreviewColor;
            
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

            foreground.transform.localScale = Vector3.one;
            background.transform.localScale = Vector3.one;
            
            _generated = false;
        }
    }
}