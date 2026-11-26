using System.Linq;
using Code;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Code
{
    using Code;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Experimental.Rendering;

    [CustomEditor(typeof(Card), true)]
    public class ItemEditor : Editor
    {
        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            var item = (Card)target;

            var sprite = item.Icon;
        
            if (sprite == null) return base.RenderStaticPreview(assetPath, subAssets, width, height);

            var rect = sprite.rect;

            int size = Mathf.Max((int)rect.width, (int)rect.height);
        
            var source = new Texture2D(size, size);

            var textureColors = sprite.texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);

            int remainingPixels = (size * size) - ((int)rect.width * (int)rect.height);

            Color[] remainingColors = new Color[remainingPixels];
        
            source.SetPixels(textureColors.Concat(remainingColors).ToArray());
        
            source.filterMode = FilterMode.Point;
        
            source.Apply();

            var rt = RenderTexture.GetTemporary(width, height);
        
            var previous = RenderTexture.active;

            try
            {
                Graphics.Blit(source, rt);

                RenderTexture.active = rt;

                var result = new Texture2D(width, height);

                result.ReadPixels(new Rect(0, 0, width, height), 0, 0);

                result.filterMode = FilterMode.Point;
            
                result.Apply();
            
                return result;
            }
            finally
            {
                RenderTexture.active = previous;
                RenderTexture.ReleaseTemporary(rt);
                DestroyImmediate(source);
            }
        }
    }
}

