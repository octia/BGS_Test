using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BGSTest
{
    public class PlayerShopping : PlayerControllerElement
    {
        public void Start()
        {
            UISystemManager.Instance.RegisterForUIEvent<UIShoppingSystem>(OnShopOpened);
        }

        public void OnShopOpened(UISystemBase uiSystem, bool wasOpened)
        {
            UIShoppingSystem shopSystem = (UIShoppingSystem)uiSystem;
            if (wasOpened)
            {
                shopSystem.OnTryBuyItem += OnTryBuyItem;
                playerController.SetCanMove(false);
            }
            else
            {
                shopSystem.OnTryBuyItem -= OnTryBuyItem;
                playerController.SetCanMove(true);
            }
        }

        private bool OnTryBuyItem(ItemSO item, int price)
        {
            Inventory inv = playerController.PlayerInventory;
            if (inv.IsAddingItemPossible(item))
            {                
                if (inv.goldAmount > price)
                {
                    inv.goldAmount -= price;
                    inv.AddItem(item);
                    return true;
                }
            }

            return false;
        }

        private void OnDestroy()
        {
            UISystemManager.Instance?.UnregisterForUIEvent<UIShoppingSystem>(OnShopOpened);
        }
    }
}