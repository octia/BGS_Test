using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    public class ExclusiveInventorySlot : InventorySlot
    {
        public OutfitTypes ExclusiveToType = OutfitTypes.Unknown;

        public ExclusiveInventorySlot(int id, OutfitTypes exclusiveType) : base(id, true)
        {
            ExclusiveToType = exclusiveType;
        }

        public override bool CanStackWith(ItemSO incomingItem, int incomingQuantity)
        {
            bool baseResult = base.CanStackWith(incomingItem, incomingQuantity);
            if (incomingItem.OutfitType == ExclusiveToType)
            {
                return baseResult;
            }
            else
            {
                return false;
            }
        }
    }
}