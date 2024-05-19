using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    [Serializable]
    public class Inventory
    {
        public Action<InventorySlot> OnInventoryChanged = delegate { };
        public int InventorySize
        {
            get
            {
                return _inventorySize;
            }
            private set
            {
                if (_inventorySize >= 0)
                {
                    _inventorySize = value;
                }
                else
                {
                    _inventorySize = 0;
                }
            }
        }
        private int _inventorySize = 0;
        public List<InventorySlot> Slots;

        public ExclusiveInventorySlot HeadOutfitSlot = new(-1, OutfitTypes.HeadOutfit);
        public ExclusiveInventorySlot BodyOutfitSlot = new(-2, OutfitTypes.BodyOutfit);

        public int goldAmount
        {
            get
            {
                return _goldAmount;
            }
            set
            {
                _goldAmount = value;
                OnInventoryChanged.Invoke(null);
            }
        }

        [SerializeField]
        private int _goldAmount = 0;

        public void Initialize(int inventorySize)
        {
            InventorySize = inventorySize;
            Slots = new List<InventorySlot>();
            for (int i = 0; i < inventorySize; i++)
            {
                Slots.Add(new InventorySlot(i));
            }
        }

        public bool IsAddingItemPossible(ItemSO toAdd)
        {
            foreach (InventorySlot slot in Slots)
            {
                if (slot.CanStackWith(toAdd, 1))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddItem(ItemSO toAdd, int amount = 1)
        {
            foreach (InventorySlot slot in Slots)
            {
                if (slot.CanStackWith(toAdd, amount))
                {
                    slot.SetItem(toAdd, slot.quantity + amount);
                    OnInventoryChanged.Invoke(slot);
                    break;
                }
            }
        }

        public bool RemoveItem(int slotID, int amount = 1)
        {
            if (Slots[slotID].quantity >= amount)
            {
                Slots[slotID].ChangeQuantity(-amount);
                OnInventoryChanged.Invoke(Slots[slotID]);
                return true;
            }
            return false;
        }

        public bool RemoveItem(ItemSO toRemove, int amount = 1)
        {
            foreach (InventorySlot slot in Slots)
            {
                if (slot.CanStackWith(toRemove, -amount))
                {
                    slot.ChangeQuantity(-amount);
                    OnInventoryChanged.Invoke(slot);
                    return true;
                }
            }
            return false;
        }

        public void TrySwapOutfit(InventorySlot slot1, InventorySlot outfitSlot)
        {
            if (!outfitSlot.IsOutfitSlot)
            {
                Debug.LogWarning("Something went wrong!");
            }
            if (outfitSlot.IsEmpty())
            {
                if (!slot1.IsEmpty())
                {
                    if (outfitSlot.CanStackWith(slot1.itemSO, 1))
                    {
                        // put on outfit
                        outfitSlot.SetItem(slot1.itemSO, 1);
                        slot1.ChangeQuantity(-1);
                        OnInventoryChanged.Invoke(slot1);
                        OnInventoryChanged.Invoke(outfitSlot);
                    }
                }
            }
            else
            {
                if (!slot1.IsEmpty())
                {
                    return; // add actual swapping later
                }
                else
                {
                    if (slot1.CanStackWith(outfitSlot.itemSO, 1))
                    {
                        // take off outfit
                        slot1.SetItem(outfitSlot.itemSO, 1);
                        outfitSlot.ChangeQuantity(-1);
                        OnInventoryChanged.Invoke(slot1);
                        OnInventoryChanged.Invoke(outfitSlot);
                    }
                }
            }
        }

    }
}
