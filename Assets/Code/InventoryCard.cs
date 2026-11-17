using System;
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
        [field:SerializeField] public RectTransform VisualRect { get; private set; }
        private Card _card;
        private float _cooldown = 0f;

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

        public void OnCarriedUpdate(Vector2 position, SpriteRenderer preview)
        {
            foreach (var hit in Physics2D.OverlapBoxAll(position,Vector2.one*0.1f,0))
            {
                if (hit.TryGetComponent(out Tile tile))
                {
                    preview.gameObject.SetActive(true);
                    VisualRect.gameObject.SetActive(false);
                    preview.transform.position = tile.transform.position + Vector3.up * 0.2f;
                    if (_card is CatCard catCard)
                    {
                        preview.sprite = catCard.Preview;

                        var newColor = new Color();
                        
                        if (tile.Cat || 
                            GameManager.Instance.Coins < catCard.Cost ||
                            _cooldown > 0f) 
                            newColor = Color.red;
                        else newColor = Color.white;

                        newColor.a = 0.5f;
                        
                        preview.color = newColor;
                    }
                    else
                    {
                        _card.Preview.GeneratePreview(preview, tile);
                    }

                    if (tile.Cat == null) ;

                    return;
                }
            }
            preview.gameObject.SetActive(false);
            VisualRect.gameObject.SetActive(true);
        }

        public void OnCardDrop(Vector2 position)
        {
            foreach (var hit in Physics2D.OverlapBoxAll(position,Vector2.one*0.1f,0))
            {
                if (hit.TryGetComponent(out Tile tile))
                {
                    
                    if (GameManager.Instance.Coins < _card.Cost) return;
                    if (_cooldown > 0f) return;
                    
                    if (_card is CatCard catCard)
                    {
                        if (tile.Cat) return;
                        var cat = Instantiate(catCard.Cat);
                        cat.Init(tile);
                    }
                    else if (_card is SpellCard spellCard)
                    {
                        var spell = Instantiate(spellCard.Spell);
                        spell.Cast(tile);
                    }
                    
                    GameManager.Instance.Coins -= _card.Cost;
                    _cooldown = _card.Cooldown;
                    return;
                }
            }
        }
    }
}