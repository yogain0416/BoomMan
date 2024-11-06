using System;
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
            // TODO 파일데이터에서 값 가져오기
            _gold.text = "";
            _posion.text = "";
        }

        public void OnClickedSettings() => _settingListener?.Invoke();
        public void OnClickedUpgrade() => _upgradeListener?.Invoke();
    }
}

