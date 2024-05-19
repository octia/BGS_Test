using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BGSTest
{
    public class UIShoppingSystem : UISystemBase
    {
        /// <summary>
        /// Called when attempting to buy an item.
        /// Inputs: Item, price of item (price may be different than the one in SO)
        /// Outputs: if item was successfully bought.
        /// </summary>
        public Func<ItemSO, int, bool> OnTryBuyItem;

        public event Action<InventorySlot, int> OnSellItem = delegate{};

        [SerializeField]
        private RectTransform _ShopSlotParent;

        [SerializeField]
        private float _shopSlotHeight = 100f; // Ideally dynamic to automatically take into account changing the size of the slot, but I'm running out of time

        [SerializeField]
        private ShopSlotController _ShopSlotPrefab;

        private Shopkeeper _shopKeep;

        private UIInventorySystem _invSystem;

        public void SetupItems(Shopkeeper shopkeep)
        {
            var _shopSO = shopkeep.GetShopkeepSO();
            for (int i = 0; i < _shopSO.SoldItems.Count; i++)
            {
                Instantiate(_ShopSlotPrefab, _ShopSlotParent).FillData(_shopSO.SoldItems[i], this);
            }
            _shopKeep = shopkeep;
            float newHeight = _shopSlotHeight * _shopSO.SoldItems.Count + 10f * (_shopSO.SoldItems.Count - 1);// the 10f is for padding. I'm in a hurry.
            _ShopSlotParent.sizeDelta = new Vector2(_ShopSlotParent.sizeDelta.x, newHeight);
        }

        public void TryPurchase(ItemSO itemSO)
        {
            if (OnTryBuyItem.Invoke(itemSO, itemSO.BuyPrice))
            {
                // If we want to remove the item from sale for example, this would be the place
            }
        }

        public override void OnClose()
        {
            for (int i = 0; i < _ShopSlotParent.childCount; i++)
            {
                Destroy(_ShopSlotParent.GetChild(i).gameObject);
            }
            _invSystem.OnSlotSelected -= SellItem;
        
        }
        public override void OnOpen()
        {
            base.OnOpen();
            _invSystem = UISystemManager.Instance.OpenUISystem<UIInventorySystem>();
            _invSystem.OnSlotSelected += SellItem;
        }

        private void SellItem(InventorySlot toSell)
        {
            if (toSell != null)
            {
                if (!toSell.IsEmpty())
                {
                    OnSellItem.Invoke(toSell, toSell.itemSO.SellPrice); // sale price specified because shopkeep might want to change it in the future
                }
            }
        }


    }
}