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
        public Card Card => _card;
        private float _cooldown = 0f;
        private Vector2 _visualsStartPosition;

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

        public void OnSelected(RectTransform container)
        {
            visualRect.SetParent(container);
            _visualsStartPosition = visualRect.position + Vector3.up * 20f;
            visualRect.position = _visualsStartPosition;
        }

        public void OnDeselected()
        {
            visualRect.SetParent(transform);
            visualRect.localPosition = Vector3.zero;
        }

        public void OnCarriedUpdate(Tile[] tiles, Vector2 canvasPos, PreviewHandler handler, bool drop = false)
        {
            bool available = _cooldown <= 0f && GameManager.Instance.Coins >= _card.Cost;

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
                visualRect.position = _visualsStartPosition;
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
                visualRect.position = canvasPos;
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

        
        
    }
}