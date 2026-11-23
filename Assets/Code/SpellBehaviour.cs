using UnityEngine;

namespace Code
{
    public abstract class SpellBehaviour : MonoBehaviour
    {
        [SerializeField] private float destroyTime = 0.5f;
        protected abstract void OnCast(params Tile[] tiles);

        public void Cast(params Tile[] tiles)
        {
            Destroy(gameObject, destroyTime);
            OnCast(tiles);
        }
    }
}