using Code;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class MoneyGenerator : Attack
{
    [SerializeField] int coinsGenerated;
    [SerializeField] private GameObject coinsPref;

    public override bool IsEnemyInRange(out IDamageable target)
    {
        target = null;
        return true;
    }

    public override void Perform()
    {
        GameManager.Instance.Coins += coinsGenerated;
        if (coinsPref)
        {
            GameObject coin = Instantiate(coinsPref, transform.position, Quaternion.identity);
            if (coin.TryGetComponent<Rigidbody2D>(out Rigidbody2D rbC))
            {
                rbC.AddForceY(2.5f, ForceMode2D.Impulse);
            }
            coin.GetComponentInChildren<TextMeshPro>().text = "+" + coinsGenerated;
            Destroy(coin, 0.5f);
        }
    }
}
