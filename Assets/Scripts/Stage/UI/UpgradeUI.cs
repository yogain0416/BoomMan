using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace App.UI
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _titleText;

        public Action _closeButtonListener = null;

        public void OpenPopup() => gameObject.SetActive(true);
        public void ClosePopup() => gameObject.SetActive(false);
        public void OnClickedCloseButton() => _closeButtonListener?.Invoke();
    }
}
