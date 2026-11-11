using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Code
{
    public class InventoryCard : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text costText;
        private Card _card;
        private float _cooldown = 0f;

        public void Init(Card card)
        {
            _card = card;
            iconImage.sprite = card.Icon;
            nameText.text = card.DisplayName;
            costText.text = card.Cost.ToString();
        }

        private void Update()
        {
            if (InputManager.Instance.Actions.Player.Click.WasPerformedThisFrame())
            {
            }
        }
    }
}