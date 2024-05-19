using System;
using System.Collections;
using System.Collections.Generic;

namespace BGSTest
{
    [Serializable]
    public class Inventory
    {
        public Action OnInventoryChanged;
        public int InventorySize = 10;
        public List<InventorySlot> Slots;
    
        public int goldAmount = 0;

        public void Initialize()
        {
            Slots = new List<InventorySlot>();
            for (int i = 0; i < InventorySize; i++)
            {
                Slots.Add(new InventorySlot());
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
                    slot.SetItem(toAdd, slot.quantity+amount);
                    break;
                }
            }
        }

        public bool RemoveItem(int slotID, int amount = 1)
        {
            if (Slots[slotID].quantity >= amount)
            {
                Slots[slotID].ChangeQuantity(-amount);
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
                    return true;
                }
            }
            return false;
        }
    
    }
}
