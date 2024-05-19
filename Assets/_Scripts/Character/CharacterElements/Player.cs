using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    /// <summary>
    /// Player class, handling player-specific things like player animations or inventory
    /// </summary>
    public class Player : Character
    {
        public Inventory PlayerInventory = new();


        // Probably would be a good idea to change separate head/body outfits into a dictionary. Same for inventory outfit slots.
        public OutfitSO bodyOutfit
        {
            get
            {
                return _bodyOutfit;
            }
            set
            {

            }
        }
        public OutfitSO headOutfit
        {
            get
            {
                return _headOutfit;
            }
            set
            {

            }
        }
        [SerializeField]
        private OutfitSO _bodyOutfit;
        [SerializeField]
        private OutfitSO _headOutfit;

        private void Start()
        {
            PlayerInventory.OnInventoryChanged += OnInventoryChanged;
        }

        private void OnInventoryChanged(InventorySlot slot)
        {
            if (slot == null)
            {
                return;
            }
            if (slot.IsOutfitSlot)
            {
                if (slot.IsEmpty())
                {
                    if (((ExclusiveInventorySlot)slot).ExclusiveToType == OutfitTypes.HeadOutfit)
                    {
                        _headOutfit = null;
                    }
                    if (((ExclusiveInventorySlot)slot).ExclusiveToType == OutfitTypes.BodyOutfit)
                    {
                        _bodyOutfit = null;
                    }
                }
                else if (slot.itemSO.OutfitAnimation)
                {
                    if (slot.itemSO.OutfitType == OutfitTypes.HeadOutfit)
                    {
                        _headOutfit = slot.itemSO.OutfitAnimation;
                    }
                    if (slot.itemSO.OutfitType == OutfitTypes.BodyOutfit)
                    {
                        _bodyOutfit = slot.itemSO.OutfitAnimation;
                    }
                }
            }
        }

    }
}