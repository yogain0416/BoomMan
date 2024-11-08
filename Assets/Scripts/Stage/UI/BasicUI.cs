using System;
using App.Data;
using TMPro;

using UnityEngine;

namespace App.UI
{
    public class BasicUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gold;
        [SerializeField] private TextMeshProUGUI _oreText_1;
        [SerializeField] private TextMeshProUGUI _oreText_2;
        [SerializeField] private TextMeshProUGUI _oreText_3;
        [SerializeField] private TextMeshProUGUI _oreText_4;

        public Action _settingListener = null;
        public Action _upgradeListener = null;

        public void SetTexts()
        {
            _gold.text = PlayerDataManager.PlayerData.gold.ToString();
            
            // TODO 하드코딩 수정필요
            _oreText_1.text = PlayerDataManager.Ore["20101"].ToString();
            _oreText_2.text = PlayerDataManager.Ore["20102"].ToString();
            _oreText_3.text = PlayerDataManager.Ore["20103"].ToString();
            _oreText_4.text = PlayerDataManager.Ore["20104"].ToString();
        }

        public void OnClickedSettings() => _settingListener?.Invoke();
        public void OnClickedUpgrade() => _upgradeListener?.Invoke();
    }
}

