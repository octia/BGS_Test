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

        public OutfitSO outfitSlot;
        public OutfitSO hairSlot;
        public OutfitSO hatSlot;


    }
}