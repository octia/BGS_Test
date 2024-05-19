using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    public class PlayerInventory : PlayerControllerElement
    {
        [SerializeField]
        private int _inventorySize = 10;


        public virtual void Start()
        {
            UISystemManager.Instance?.RegisterForUIEvent<UIInventorySystem>(OnInventoryOpened);
            playerController.PlayerInventory.Initialize();
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