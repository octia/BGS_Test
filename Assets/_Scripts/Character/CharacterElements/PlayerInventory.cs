using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    public class PlayerInventory : PlayerControllerElement
    {
        public event Action<InventorySlot, bool> onSlotSelected;

        [SerializeField]
        private int _inventorySize = 10;

        private bool _isInventoryOpen = false;


        private UIInventorySystem _inventorySystem;

        public virtual void Start()
        {
            UISystemManager.Instance?.RegisterForUIEvent<UIInventorySystem>(OnInventoryOpened);
            playerController.PlayerInventory.Initialize(_inventorySize);
            playerController.PlayerInventory.OnInventoryChanged += OnInventoryChanged;
        }

        public void OnInventoryOpened(UISystemBase uiSystem, bool wasOpened)
        {
            _isInventoryOpen = wasOpened;
            if (wasOpened)
            {
                _inventorySystem = (UIInventorySystem)uiSystem;
                _inventorySystem.FillData(playerController.PlayerInventory);
            }
        }


        private void OnInventoryChanged(InventorySlot slot)
        {
            if (_isInventoryOpen)
            {
                _inventorySystem.RefreshView();
            }
        }


        private void OnDestroy()
        {
            UISystemManager.Instance?.UnregisterForUIEvent<UIInventorySystem>(OnInventoryOpened);
        }

    }
}