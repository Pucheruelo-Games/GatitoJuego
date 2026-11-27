using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class ShopCardPack : MonoBehaviour
    {
        [SerializeField] private Image image;
        private CardPack _pack;

        public void Init(CardPack pack)
        {
            _pack = pack;
            image.sprite = _pack.Icon;
        }

        public bool TryOpen(out Card card)
        {
            card = null;
            if (GameManager.Instance.Coins < _pack.Cost) return false;

            GameManager.Instance.Coins -= _pack.Cost;

            card = _pack.GetRandomCard();

            if (card)
            {
                Destroy(this.gameObject);
            }

            return card != null;
        }
    }
}