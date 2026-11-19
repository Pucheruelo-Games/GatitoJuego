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
        [SerializeField] private RectTransform visualRect;
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

        public void OnCarriedUpdate(Vector2 worldPos, Vector2 canvasPos, PreviewHandler handler)
        {
            visualRect.transform.position = canvasPos;
            foreach (var hit in Physics2D.OverlapBoxAll(worldPos,Vector2.one*0.1f,0))
            {
                if (hit.TryGetComponent(out Tile tile))
                {
                    visualRect.gameObject.SetActive(false);
                    handler.Init(tile, _cooldown <= 0f && GameManager.Instance.Coins >= _card.Cost);
                    _card.GeneratePreview(handler);
                    return;
                }
            }
            visualRect.gameObject.SetActive(true);
        }

        public void OnCardDrop(Vector2 worldPos, Vector2 canvasPos, PreviewHandler handler)
        {
            foreach (var hit in Physics2D.OverlapBoxAll(worldPos,Vector2.one * 0.1f,0))
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
            visualRect.gameObject.SetActive(true);
            visualRect.localPosition = Vector3.zero;
            handler.ReleasePreview();
        }
    }
}