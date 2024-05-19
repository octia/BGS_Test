using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        [SerializeField]
        private RectTransform _ShopSlotParent;

        [SerializeField]
        private float _shopSlotHeight = 100f; // Ideally dynamic to automatically take into account changing the size of the slot, but I'm running out of time

        [SerializeField]
        private ShopSlotController _ShopSlotPrefab;


        public void SetupItems(ShopkeepSO shopkeepSO)
        {
            while (_ShopSlotParent.childCount > 0)
            {
                Destroy(_ShopSlotParent.GetChild(0));
            }
            for (int i = 0; i < shopkeepSO.SoldItems.Count; i++)
            {
                Instantiate(_ShopSlotPrefab, _ShopSlotParent).FillData(shopkeepSO.SoldItems[i], this);
            }

            float newHeight =  _shopSlotHeight * shopkeepSO.SoldItems.Count + 10f * (shopkeepSO.SoldItems.Count-1);// the 10f is for padding. I'm in a hurry.
            _ShopSlotParent.sizeDelta = new Vector2(_ShopSlotParent.sizeDelta.x, newHeight); 
        }

        public void TryPurchase(ItemSO itemSO)
        {
            if (OnTryBuyItem.Invoke(itemSO, itemSO.BuyPrice))
            {
            }
        }

        public override void OnOpen()
        {
            base.OnOpen();
            UISystemManager.Instance.OpenUISystem<UIInventorySystem>();
        }


    }
}