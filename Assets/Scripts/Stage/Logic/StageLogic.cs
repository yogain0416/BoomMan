using App.Player;
using App.UI;

using BoomManData;

using UnityEngine;

namespace App.Logic
{
    public class StageLogic : MonoBehaviour
    {
        [SerializeField] private BasicUI _basicUI;
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private SettingUI _settingUI;
        [SerializeField] private PlayerMovement _playerMovement;
        

        private void Awake()
        {
            SetLiseners();
            SetTexts();
        }

        private void Start()
        {
            StartPlayerUI();
        }

        private void SetTexts()
        {
            _basicUI.SetTexts();
        }

        private void SetLiseners()
        {
            _basicUI._settingListener = OpenSettingPopup;
            _basicUI._upgradeListener = OnClickedUpgrade;

            _settingUI._closeButtonListener = CloseSettingPopup;
        }

        private void StartPlayerUI()
        {
            StartCoroutine(_playerUI.BoomTimer());
            StartCoroutine(_playerUI.Capacity());
        }

        private void OnClickedUpgrade()
        {
            // TODO 창 띄우기
            Debug.Log("OnClickedUpgrade");
        }

        private void OpenSettingPopup()
        {
            _settingUI.OpenPopup();
            _playerMovement.BlockMovement = true;
            Debug.Log("OnClickedSettings");
        }

        private void CloseSettingPopup()
        {
            _settingUI.ClosePopup();
            _playerMovement.BlockMovement = false;
        }
    }
}

