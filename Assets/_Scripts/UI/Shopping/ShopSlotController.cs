using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BGSTest
{
    public class ShopSlotController : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _name;
        [SerializeField]
        private TMP_Text _price;
        [SerializeField]
        private Image _icon;

        private ItemSO _itemSO;

        private UIShoppingSystem _shoppingSystem;

        public void FillData(ItemSO itemSO, UIShoppingSystem shoppingSystem)
        {
            _shoppingSystem = shoppingSystem;
            _itemSO = itemSO;
            _name.text = itemSO.name;
            _price.text = itemSO.BuyPrice.ToString();
            _icon.sprite = itemSO.icon;
        }

        public void OnClick()
        {
            _shoppingSystem.TryPurchase(_itemSO);
        }


    }
}