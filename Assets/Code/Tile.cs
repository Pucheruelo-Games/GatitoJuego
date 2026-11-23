using System;
using UnityEngine;

namespace Code
{
    public class Tile : MonoBehaviour
    {
        public CatBehaviour Cat;
        [SerializeField] private Tile up, down, right, left;
        public Tile Up => up;
        public Tile Down => down;
        public Tile Right => right;
        public Tile Left => left;
        
        [ContextMenu("Set Adjacent Tiles")]   
        private void SetAdjacent()
        {
            foreach (var hit in Physics2D.RaycastAll(transform.position, Vector2.up))
            {
                if (hit.collider.gameObject.TryGetComponent(out Tile tile) && tile != this)
                {
                    up = tile;
                    break;
                }
            }
            
            foreach (var hit in Physics2D.RaycastAll(transform.position, Vector2.down))
            {
                if (hit.collider.gameObject.TryGetComponent(out Tile tile) && tile != this)
                {
                    down = tile;
                    break;
                }
            }
            
            foreach (var hit in Physics2D.RaycastAll(transform.position, Vector2.left))
            {
                if (hit.collider.gameObject.TryGetComponent(out Tile tile) && tile != this)
                {
                    left = tile;
                    break;
                }
            }
            
            foreach (var hit in Physics2D.RaycastAll(transform.position, Vector2.right))
            {
                if (hit.collider.gameObject.TryGetComponent(out Tile tile) && tile != this)
                {
                    right = tile;
                    break;
                }
            }
        }
    }
}