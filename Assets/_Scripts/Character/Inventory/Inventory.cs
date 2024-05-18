using System;
using System.Collections;
using System.Collections.Generic;

namespace BGSTest
{
    [Serializable]
    public class Inventory
    {
        public Action OnInventoryChanged;

        
        public List<InventorySlot> Slots;
    
    }
}
