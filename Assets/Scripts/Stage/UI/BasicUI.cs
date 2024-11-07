using System;
using App.Data;
using TMPro;

using UnityEngine;

namespace App.UI
{
    public class BasicUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gold;
        [SerializeField] private TextMeshProUGUI _posion;

        public Action _settingListener = null;
        public Action _upgradeListener = null;

        public void SetTexts()
        {
            // TODO
            _gold.text = PlayerDataManager.PlayerData.gold.ToString();
            _posion.text = "";
        }

        public void OnClickedSettings() => _settingListener?.Invoke();
        public void OnClickedUpgrade() => _upgradeListener?.Invoke();
    }
}

