using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    public class UIShoppingSystem : UISystemBase
    {
        public void SetupItems()
        {
            
        }

        public override void OnOpen()
        {
            base.OnOpen();
            UISystemManager.Instance.OpenUISystem<UIInventorySystem>();
        }

        /// <summary>
        /// Called when attempting to buy an item.
        /// Inputs: GUID of item, price of item (price may be different than the one in SO)
        /// Outputs: if item was successfully bought.
        /// </summary>
        public Func<ItemSO, int, bool> OnTryBuyItem;

    }
}