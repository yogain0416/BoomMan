using TMPro;

using UnityEngine;

using App.Initialization;
using App.Data;
using UnityEngine.UI;
using App.Player;

namespace App.UI
{
    public class UpgradeElementUI : MonoBehaviour
    {
        // TODO : 생성 할 때 icon도 각자 다르게 들어가게 나중에 만들자
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descText;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Button _button;
        [SerializeField] private Sprite _graySprite;
        [SerializeField] private Sprite _greenSprite;

        [SerializeField] private PlayerBoom _playerBoom;
        [SerializeField] private BasicUI _basicUI;

        private string _key;
        private float _value;
        private BoomManData.Upgrade _data;
        private string _nextLevelID;
        private BoomManData.Upgrade _nextData;

        public void SetContent(string key, float value)
        {
            _key = key;
            _value = value;
            _data = BoomManData.Upgrade.UpgradeMap[PlayerDataManager.PlayerData.upgradeId[key].ToString()];
            _nextLevelID = _data.nextLevelId;
            bool isMax = _nextLevelID.Equals("0");

            if (isMax == false) _nextData = BoomManData.Upgrade.UpgradeMap[_nextLevelID];


            // Text
            _titleText.text = LanguageManager.Instance.GetString(_data.upgradeTitleKey);
            if (isMax)
            {
                // TODO 하드코딩 개선
                _descText.text = "MAX LEVEL";
                _buttonText.text = "MAX";
            }
            else
            {
                _descText.text = LanguageManager.Instance.GetString(_data.upgradeDescKey
                                , value * (1 + _data.abilityAmount1 / 100)
                                , value * (1 + _nextData.abilityAmount1 / 100));
                _buttonText.text = _nextData.goldCost.ToString();
            }

            // Button
            if (isMax == false && PlayerDataManager.PlayerData.gold >= _nextData.goldCost)
            {
                _buttonImage.sprite = _greenSprite;
                _button.interactable = true;
            }
            else
            {
                _buttonImage.sprite = _graySprite;
                _button.interactable = false;
            }
        }

        public void OnClickedUpgrade()
        {
            Debug.Log("업그레이드 버튼");

            // 골드 감소
            PlayerDataManager.PlayerData.gold -= _nextData.goldCost;

            // 골드 UI 갱신
            _basicUI.SetTexts();

            // 플레이어 데이터 갱신
            PlayerDataManager.PlayerData.upgradeId[_key] = _nextLevelID;

            // 업그레이드 관련 보여지는 것들 갱신
            if (_key.Equals("boomRange")) _playerBoom.SetBoomRange();

            // 업그레이드 UI 갱신
            SetContent(_key, _value);
        }
    }
}