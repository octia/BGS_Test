using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    [Serializable]
    public class Inventory
    {
        public Action OnInventoryChanged = delegate { };
        public int InventorySize = 10;
        public List<InventorySlot> Slots;

        public InventorySlot HeadOutfitSlot = new InventorySlot(-1, true);
        public InventorySlot BodyOutfitSlot = new InventorySlot(-2, true);

        public int goldAmount
        {
            get
            {
                return _goldAmount;
            }
            set
            {
                _goldAmount = value;
                OnInventoryChanged.Invoke();
            }
        }

        [SerializeField]
        private int _goldAmount = 0;

        public void Initialize()
        {
            Slots = new List<InventorySlot>();
            for (int i = 0; i < InventorySize; i++)
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
                    OnInventoryChanged.Invoke();
                    break;
                }
            }
        }

        public bool RemoveItem(int slotID, int amount = 1)
        {
            if (Slots[slotID].quantity >= amount)
            {
                Slots[slotID].ChangeQuantity(-amount);
                OnInventoryChanged.Invoke();
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
                    OnInventoryChanged.Invoke();
                    return true;
                }
            }
            return false;
        }

        public void TrySwapOutfit(InventorySlot slot1, InventorySlot outfitSlot)
        {
            if (!outfitSlot.MaxQuantityIsOne)
            {
                Debug.LogWarning("Something went wrong!");
            }
            if (outfitSlot.IsEmpty())
            {
                if (!slot1.IsEmpty())
                {
                    // put on outfit
                    outfitSlot.SetItem(slot1.itemSO, 1);
                    outfitSlot.ChangeQuantity(-1);
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
                    // take off outfit
                    slot1.SetItem(slot1.itemSO, 1);
                    outfitSlot.ChangeQuantity(-1);
                }
            }
        }

    }
}
