using System;
using UnityEngine;

namespace Code
{
    public class Tile : MonoBehaviour
    {
        public CatBehaviour Cat;
        private Tile _up, _down, _right, _left;
        public Tile Up => _up;
        public Tile Down => _down;
        public Tile Right => _right;
        public Tile Left => _left;
        
        [ContextMenu("Set Adjacent Tiles")]   
        private void SetAdjacent()
        {
            foreach (var hit in Physics2D.RaycastAll(transform.position, Vector2.up))
            {
                if (hit.collider.gameObject.TryGetComponent(out Tile tile) && tile != this)
                {
                    _up = tile;
                    break;
                }
            }
            
            foreach (var hit in Physics2D.RaycastAll(transform.position, Vector2.down))
            {
                if (hit.collider.gameObject.TryGetComponent(out Tile tile) && tile != this)
                {
                    _down = tile;
                    break;
                }
            }
            
            foreach (var hit in Physics2D.RaycastAll(transform.position, Vector2.left))
            {
                if (hit.collider.gameObject.TryGetComponent(out Tile tile) && tile != this)
                {
                    _left = tile;
                    break;
                }
            }
            
            foreach (var hit in Physics2D.RaycastAll(transform.position, Vector2.right))
            {
                if (hit.collider.gameObject.TryGetComponent(out Tile tile) && tile != this)
                {
                    _right = tile;
                    break;
                }
            }
        }
    }
}