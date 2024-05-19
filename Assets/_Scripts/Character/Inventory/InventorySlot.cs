using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    [Serializable]
    public class InventorySlot
    {
        public ItemSO itemSO = null;
        public int quantity 
        { 
            get
            { 
                return itemSO ? _quantity : 0;
            }
            set
            {
                _quantity = value;
            }
        }

        public int slotID = 0;
        public InventorySlot(int id, bool isOutfitSlot = false)
        {
            slotID = id;
            IsOutfitSlot = isOutfitSlot;
        }

        [SerializeField]
        protected int _quantity = 0;

        public int maxQuantity => IsOutfitSlot ? 1 : itemSO ? itemSO.MaxStackSize : 0;

        public bool IsOutfitSlot = false;

        public void SetItem(ItemSO newItem, int newQuantity)
        {
            itemSO = newItem;
            if (!newItem)
            {
                quantity = 0;
            }
            else
            {
                quantity = newQuantity;
            }
        }

        public virtual bool CanStackWith(ItemSO incomingItem, int incomingQuantity)
        {
            if (incomingItem == null)
            {
                return false;
            }
            if (incomingQuantity == 0)
            {
                return false; 
            }

            if (!itemSO)
            {
                return true; // can always stack when empty 
            }

            if (incomingItem == itemSO)
            {
                int newQuantity = quantity + incomingQuantity; 
                if (newQuantity > maxQuantity)
                {
                    return false;
                }
                else // always true for incomingQuantity below zero.
                {
                    return true;
                }
            }
            return false;
        }

        public void ChangeQuantity(int toChange)
        {
            SetQuantity(quantity + toChange);
        }

        public void SetQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
            {
                itemSO = null;
                quantity = 0;
            }
            else
            {
                quantity = newQuantity;
            }
        }

        public bool IsEmpty()
        {
            if (quantity == 0 || !itemSO || itemSO == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}