using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    public enum OutfitTypes
    {
        Unknown = 0,HeadOutfit,BodyOutfit
    }
    [CreateAssetMenu(fileName = "New Item", menuName = "Scriptables/New Item")]
    public class ItemSO : ScriptableObject
    {

        [ScriptableObjectId]
        public string ItemID;
        public string ItemName = "New Item";
        public int MaxStackSize = 1;
        public Sprite icon => OutfitAnimation.GetIdleFrame(Vector2Int.down);
        public int BuyPrice = 10;
        public int SellPrice = 5;
        public OutfitSO OutfitAnimation;        
        public OutfitTypes OutfitType;

    }
}