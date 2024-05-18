using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    public class Shopkeeper : MonoBehaviour, IInteractable
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Interact()
        {
            UIShoppingSystem shopSystem = UISystemManager.Instance.OpenUISystem<UIShoppingSystem>();
            
        }
    }
}