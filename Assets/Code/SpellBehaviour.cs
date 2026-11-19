using UnityEngine;

namespace Code
{
    public abstract class SpellBehaviour : MonoBehaviour
    {
        public abstract void Cast(Tile tile);
    }
}