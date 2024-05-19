using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;


namespace BGSTest
{
    public class UIInventorySystem : UISystemBase
    {

        public event Action<InventorySlot> OnSlotSelected = delegate { };

        [SerializeField]
        private ItemSlotController _HeadOutfitSlot;
        [SerializeField]
        private ItemSlotController _BodyOutfitSlot;

        [SerializeField]
        private TMP_Text _goldText;

        [SerializeField]
        private RectTransform _itemSlotParent;

        [SerializeField]
        private ItemSlotController _itemSlotPrefab;

        [SerializeField]
        private float _itemSlotHeight = 100f; // Ideally dynamic to automatically take into account changing the size of the slot, but I'm running out of time
        [SerializeField]
        private int _itemSlotsPerRow = 5; // again, ideally dynamic

        private Inventory _inventory;

        private List<ItemSlotController> _itemSlots = new List<ItemSlotController>();

        public void FillData(Inventory inventory)
        {
            _inventory = inventory;
            _itemSlots = new List<ItemSlotController>();
            int slotCount = inventory.Slots.Count;
            for (int i = 0; i < slotCount; i++)
            {
                var newSlot = Instantiate(_itemSlotPrefab, _itemSlotParent);
                newSlot.FillData(inventory.Slots[i], this);
                _itemSlots.Add(newSlot);
            }

            _HeadOutfitSlot.FillData(inventory.HeadOutfitSlot, this);
            _BodyOutfitSlot.FillData(inventory.BodyOutfitSlot, this);

            int rowCount = Mathf.RoundToInt(Mathf.Floor(((float)inventory.Slots.Count) / _itemSlotsPerRow));

            float newHeight = _itemSlotHeight * rowCount + 10f * (rowCount - 1);// the 10f is for padding. I'm in a hurry.
            _itemSlotParent.sizeDelta = new Vector2(_itemSlotParent.sizeDelta.x, newHeight);


            FillGold(_inventory);
        }

        public override void OnClose()
        {
            for (int i = 0; i < _itemSlotParent.childCount; i++)
            {
                Destroy(_itemSlotParent.GetChild(i).gameObject);
            }
        }

        void FillGold(Inventory inventory)
        {
            if (inventory != null)
            {
                _goldText.text = inventory.goldAmount.ToString();
            }
        }

        public void RefreshView()
        {
            foreach (var slot in _itemSlots)
            {
                slot.RefreshView();
            }
            _HeadOutfitSlot.RefreshView();
            _BodyOutfitSlot.RefreshView();
            FillGold(_inventory);
        }

        public void ClickedSlot(InventorySlot _slot)
        {
            bool swappingItems = false;
            if (_HeadOutfitSlot.IsSelected)
            {
                _inventory.TrySwapOutfit(_slot, _inventory.HeadOutfitSlot);
                _HeadOutfitSlot.IsSelected = false;
                _BodyOutfitSlot.IsSelected = false;
                swappingItems = true;
            }
            if (_BodyOutfitSlot.IsSelected)
            {
                _inventory.TrySwapOutfit(_slot, _inventory.BodyOutfitSlot);
                _HeadOutfitSlot.IsSelected = false;
                _BodyOutfitSlot.IsSelected = false;
                swappingItems = true;
            }

            UnselectAllSlots();
            RefreshView();
            if (!swappingItems)
            {
                OnSlotSelected.Invoke(_slot);
            }
        }

        private void UnselectAllSlots()
        {
            foreach (var slot in _itemSlots)
            {
                slot.Unselect();
            }
        }


    }
}