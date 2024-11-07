using System;
using App.Player;
using App.UI;

using UnityEngine;

namespace App.Logic
{
    public class StageLogic : MonoBehaviour
    {
        [SerializeField] private BasicUI _basicUI;
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private SettingUI _settingUI;
        [SerializeField] private UpgradeUI _upgradeUI;

        [SerializeField] private PlayerMovement _playerMovement;
        

        private void Awake()
        {
            SetLiseners();
        }

        private void SetLiseners()
        {
            _basicUI._settingListener = OpenSettingPopup;
            _basicUI._upgradeListener = OpenUpgradePopup;

            _settingUI._closeButtonListener = CloseSettingPopup;
            _upgradeUI._closeButtonListener = CloseUpgradePopup;
        }

        private void OpenUpgradePopup()
        {
            _upgradeUI.OpenPopup();
            // _playerMovement.BlockMovement = true;
            Debug.Log("OnClickedUpgrade");
        }

        private void OpenSettingPopup()
        {
            _settingUI.OpenPopup();
            // _playerMovement.BlockMovement = true;
            Debug.Log("OnClickedSettings");
        }

        private void CloseSettingPopup()
        {
            // _playerMovement.BlockMovement = false;
            _settingUI.ClosePopup();
        }
        
        private void CloseUpgradePopup()
        {
            // _playerMovement.BlockMovement = false;
            _upgradeUI.ClosePopup();
        }
    }
}

