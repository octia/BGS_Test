using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BGSTest
{
    public class ItemSlotController : MonoBehaviour
    {
        public bool IsSelected = false;
        public bool IsSpecialSlot = false;

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TMP_Text _amount;

        private UIInventorySystem _invSystem;

        private InventorySlot _slot;

        public void FillData(InventorySlot slot, UIInventorySystem inventorySystem)
        {
            _icon.sprite = null;
            _icon.enabled = false;
            _amount.gameObject.SetActive(false);
            if (slot != null)
            {
                if (slot.itemSO)
                {
                    _icon.sprite = slot.itemSO.icon;
                    _icon.enabled = true;
                    _amount.gameObject.SetActive(true);
                    _amount.text = slot.quantity.ToString();
                }
            }
            _slot = slot;
            _invSystem = inventorySystem;
        }

        public void RefreshView()
        {
            FillData(_slot, _invSystem);
        }


        public void OnClick()
        {
            if (_invSystem != null)
            {
                IsSelected = !IsSelected;
                if (!IsSpecialSlot)
                {
                    _invSystem.ClickedSlot(_slot);
                }
            }
        }

        public void Unselect()
        {
            IsSelected = false;
        }
    }
}