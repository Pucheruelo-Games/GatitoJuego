using System;
using TMPro;
using UnityEngine;


namespace Code
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] Table table;
        [SerializeField] TextMeshProUGUI healthUI;
        [SerializeField] TextMeshProUGUI coinsUI;
        private void OnEnable()
        {
            table.OnHealthChange += UpdateHealth;
            GameManager.Singleton.OnCoinsChange += UpdateCoins;
        }
        private void OnDisable()
        {
            table.OnHealthChange -= UpdateHealth;
            GameManager.Singleton.OnCoinsChange -= UpdateCoins;

        }
        private void UpdateCoins(int coins) => coinsUI.text = coins.ToString();
        private void UpdateHealth(int health) => healthUI.text = health.ToString();
    }
}