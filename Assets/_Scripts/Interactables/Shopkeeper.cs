using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    public class Shopkeeper : MonoBehaviour, IInteractable
    {

        [SerializeField]
        private ShopkeepSO _shopkeepSO;

        private bool _isOpen;
        public void Interact()
        {
            if (!_isOpen)
            {
                UIShoppingSystem shopSystem = UISystemManager.Instance?.OpenUISystem<UIShoppingSystem>();
                if (shopSystem)
                {
                    shopSystem.SetupItems(_shopkeepSO);
                }
            }
        }
    }
}