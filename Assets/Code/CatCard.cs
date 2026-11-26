using UnityEditor;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "CatCard", menuName = "Card/CatCard")]
    public class CatCard : Card
    {
        [SerializeField] private CatBehaviour cat;
        public CatBehaviour Cat => cat;
        [SerializeField] private Sprite preview;
        public Sprite Preview => preview;
        public override void GeneratePreview(PreviewHandler handler)
        {
            handler.GenerateCatPreview(this);
        }
        
        [ContextMenu("Look For Behaviour")]
        private void LookForBehaviour()
        {
            foreach (string guid in AssetDatabase.FindAssets("t:prefab "+name))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (AssetDatabase.LoadAssetAtPath<GameObject>(path).TryGetComponent(out CatBehaviour catPrefab))
                {
                    cat = catPrefab;
                }
            }
        }
    }
}