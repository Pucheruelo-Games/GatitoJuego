using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class InventoryCard : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private Image cooldownMask;
        [SerializeField] private RectTransform visualRect;
        private Card _card;
        private float _cooldown = 0f;
        private LayerMask _tileLayerMask;

        private void Awake()
        {
            _tileLayerMask = LayerMask.GetMask("Tile");
        }

        public void Init(Card card)
        {
            _card = card;
            iconImage.sprite = card.Icon;
            nameText.text = card.DisplayName;
            costText.text = card.Cost.ToString();
            cooldownMask.fillAmount = Mathf.Clamp01(_cooldown / _card.Cooldown);
        }

        private void Update()
        {
            if (_cooldown > 0f)
            {
                _cooldown -= Time.deltaTime;
                cooldownMask.fillAmount = Mathf.Clamp01(_cooldown / _card.Cooldown);
                if (_cooldown <= 0f) _cooldown = 0f;
            }
        }

        public void OnCarriedUpdate(Vector2 worldPos, Vector2 canvasPos, PreviewHandler handler, bool drop = false)
        {
            bool available = _cooldown <= 0f && GameManager.Instance.Coins >= _card.Cost;
            Tile[] tiles = null;
            foreach (var hit in Physics2D.OverlapBoxAll(worldPos, Vector2.one * 0.1f, 0, _tileLayerMask))
            {
                if (!hit.TryGetComponent(out Tile tile)) continue;
                
                tiles = GetAreaTiles(tile);
                
                break;
            }

            if (tiles != null && (_card is CatCard || (_card is SpellCard s && s.CatPreventCast)))
            {
                foreach (var t in tiles)
                {
                    if (t.Cat)
                    {
                        available = false;
                        break;
                    }
                }
            }

            if (drop)
            {
                visualRect.gameObject.SetActive(true);
                visualRect.localPosition = Vector3.zero;
                handler.ReleasePreview();

                if (!available) return;
                if (tiles == null) return;
                    
                if (_card is CatCard catCard)
                {
                    var cat = Instantiate(catCard.Cat);
                    cat.Init(tiles);
                }
                else if (_card is SpellCard spellCard)
                {
                    var spell = Instantiate(spellCard.Spell);
                    spell.Cast(tiles);
                }
                GameManager.Instance.Coins -= _card.Cost;
                _cooldown = _card.Cooldown;
            }
            else
            {
                visualRect.transform.position = canvasPos;
                if (tiles == null)
                {
                    visualRect.gameObject.SetActive(true);
                    handler.ReleasePreview();
                }
                else
                {
                    visualRect.gameObject.SetActive(false);
                    handler.Init(available, tiles);
                    _card.GeneratePreview(handler);
                }
            }
        }

        private Tile[] GetAreaTiles(Tile center, int lookBack = 0)
        {
            if (!center) return null;
            
            if (_card.Width <= 1 && _card.Height <= 1)
            {
                return new []{center};
            }

            if (lookBack >= 16) return null;
                    
            var right = center;
            var left = center;
                    
            var up = center;
            var down = center;
                    
            List<Tile> tileList = new();
                
            tileList.Add(center);

            for (int x = 1; x < _card.Width; x++)
            {
                if (x % 2 == 0)
                {
                    if (right.Right) right = right.Right;
                    else return GetAreaTiles(center.Left, lookBack+1);
                    
                    tileList.Add(right);
                }
                else
                {
                    if (left.Left) left = left.Left;
                    else return GetAreaTiles(center.Right, lookBack+1);
                    
                    tileList.Add(left);
                }
                        
                for (int y = 1; y < _card.Height; y++)
                {
                    if (y % 2 == 0)
                    {
                        if (down.Down) down = down.Down;
                        else return GetAreaTiles(center.Up, lookBack+1);
                        
                        tileList.Add(down);
                    }
                    else
                    {
                        if (up.Up) up = up.Up;
                        else return GetAreaTiles(center.Down, lookBack+1);
                        
                        tileList.Add(up);
                    }
                }
            }
            
            return tileList.ToArray();
        }
        
    }
}