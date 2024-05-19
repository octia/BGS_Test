using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    public class PlayerInventory : PlayerControllerElement
    {

        public virtual void Start()
        {
            UISystemManager.Instance?.RegisterForUIEvent<UIInventorySystem>(OnInventoryOpened);
        }

        public void OnInventoryOpened(UISystemBase uiSystem, bool wasOpened)
        {
            if (wasOpened)
            {
                UIInventorySystem inventorySystem = (UIInventorySystem)uiSystem;
                inventorySystem.FillData(playerController.PlayerInventory);
            }
        }



        private void OnDestroy()
        {
            UISystemManager.Instance?.UnregisterForUIEvent<UIInventorySystem>(OnInventoryOpened);
        }

    }
}