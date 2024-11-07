using System;
using System.Collections;
using System.Collections.Generic;
using App.Initialization;
using TMPro;
using UnityEngine;

namespace App.UI
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _titleText;

        public void SetTexts()
        {
            _titleText.text = LanguageManager.Instance.GetString("Str_Upgrade_Title");
        }

        public Action _closeButtonListener = null;

        public void OpenPopup() => gameObject.SetActive(true);
        public void ClosePopup() => gameObject.SetActive(false);
        public void OnClickedCloseButton() => _closeButtonListener?.Invoke();
    }
}
