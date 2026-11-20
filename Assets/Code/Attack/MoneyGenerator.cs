using Code;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyGenerator : Attack
{
    [SerializeField] int coinsGenerated;
    public override bool IsEnemyInRange(out IDamageable target)
    {
        target = null;
        return true;
    }

    public override void Perform()
    {
        GameManager.Instance.Coins += coinsGenerated;
    }
}
