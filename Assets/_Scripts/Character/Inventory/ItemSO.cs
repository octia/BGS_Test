using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{

    [CreateAssetMenu(fileName = "New Item", menuName = "Scriptables/New Item")]
    public class ItemSO : ScriptableObject
    {

        [ScriptableObjectId]
        public string ItemID;
        public string ItemName = "New Item";
        public int MaxStackSize = 1;
        public Sprite icon;
        public int BuyPrice = 10;
        public int SellPrice = 5;


    }
}