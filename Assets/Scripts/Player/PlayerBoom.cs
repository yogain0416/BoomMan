using App.UI;
using App.Data;

using System.Collections;

using UnityEngine;

namespace App.Player
{
    public class PlayerBoom : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private BoomRangeUI _rangeUI;

        private IEnumerator Start()
        {
            // PlayerDataManager가 완전히 로드될 때까지 대기
            yield return new WaitUntil(() => PlayerDataManager.IsDataLoaded);

            StartCoroutine(_playerUI.Boom(_particleSystem));
            StartCoroutine(_playerUI.Capacity());
            SetBoomRange();
        }

        private void SetBoomRange()
        {
            float range = PlayerDataManager.PlayerData.boomRange * (1 + BoomManData.Upgrade.UpgradeMap[PlayerDataManager.PlayerData.upgradeId["boomRange"]].abilityAmount1 / 100);
            _rangeUI.SetRange(range);
        }
    }
}

