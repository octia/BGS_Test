using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BGSTest
{
    public class PlayerShopping : PlayerControllerElement
    {
        public void Start()
        {
            UISystemManager.Instance?.RegisterForUIEvent<UIShoppingSystem>(OnShopOpened);
        }

        public void OnShopOpened(UISystemBase uiSystem, bool wasOpened)
        {
            UIShoppingSystem shopSystem = (UIShoppingSystem)uiSystem;
            if (wasOpened)
            {
                shopSystem.OnTryBuyItem += OnTryBuyItem;
                shopSystem.OnSellItem += OnSellItem;
                playerController.SetCanMove(false);
            }
            else
            {
                shopSystem.OnTryBuyItem -= OnTryBuyItem;
                shopSystem.OnSellItem -= OnSellItem;
                playerController.SetCanMove(true);
            }
        }

        private bool OnTryBuyItem(ItemSO item, int price)
        {
            Inventory inv = playerController.PlayerInventory;
            if (inv.IsAddingItemPossible(item))
            {            
                if (inv.goldAmount >= price)
                {
                    inv.goldAmount -= price;
                    inv.AddItem(item);
                    return true;
                }
            }

            return false;
        }

        private void OnSellItem(InventorySlot item, int price)
        {
            Inventory inv = playerController.PlayerInventory;
            if (inv.RemoveItem(item.slotID, 1))
            {
                inv.goldAmount += price;
            }

        }

        private void OnDestroy()
        {
            UISystemManager.Instance?.UnregisterForUIEvent<UIShoppingSystem>(OnShopOpened);
        }
    }
}