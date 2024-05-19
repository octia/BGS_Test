using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{

    [CreateAssetMenu(fileName = "New Shopkeep", menuName = "Scriptables/New Shopkeep")]
    public class ShopkeepSO : ScriptableObject
    {
        public string ShopkeepName;
        public List<ItemSO> SoldItems;
    }
}