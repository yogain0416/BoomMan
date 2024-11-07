using System.Collections;
using System.Collections.Generic;

using App.UI;
using App.Data;
using App.Player;

using UnityEngine;

namespace App.Logic
{
    public class StageLogic : MonoBehaviour
    {
        [SerializeField] private BasicUI _basicUI;
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private SettingUI _settingUI;
        [SerializeField] private UpgradeUI _upgradeUI;
        [SerializeField] private List<UpgradeElementUI> _upgradeElementsUI;

        [SerializeField] private PlayerMovement _playerMovement;
        

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => PlayerDataManager.IsDataLoaded);
            
            SetLiseners();
            SetTexts();
        }

        private void SetTexts()
        {
            _basicUI.SetTexts();
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
            SetContents();
        }

        // TODO 하드코딩 변경 필요
        private void SetContents()
        {
            _upgradeElementsUI[0].SetContent("boomRange", PlayerDataManager.PlayerData.boomRange);
            _upgradeElementsUI[1].SetContent("boomPower", PlayerDataManager.PlayerData.boomPower);
            _upgradeElementsUI[2].SetContent("boomSpeed", PlayerDataManager.PlayerData.boomSpeed);
            _upgradeElementsUI[3].SetContent("capacity", PlayerDataManager.PlayerData.capacity);
            _upgradeElementsUI[4].SetContent("moveSpeed", PlayerDataManager.PlayerData.speed);
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

