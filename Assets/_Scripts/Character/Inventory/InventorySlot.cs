using System;
using System.Collections;
using System.Collections.Generic;

namespace BGSTest
{
    [Serializable]
    public class InventorySlot
    {
        public ItemSO ItemSO;
        public int quantity = 0;

        public bool IsEmpty()
        {
            if (quantity == 0 || (!ItemSO))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}