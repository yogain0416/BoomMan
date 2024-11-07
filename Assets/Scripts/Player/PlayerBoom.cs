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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "NPC")
            {
                // TODO 연구소 띄우기
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Ore" && _playerUI.HasBoom)
            {
                Debug.Log("광석");
                _playerUI.HasBoom = false;
                var ore = other.gameObject.GetComponent<Ore>();
                float damage = PlayerDataManager.PlayerData.boomPower * (1 + BoomManData.Upgrade.UpgradeMap[PlayerDataManager.PlayerData.upgradeId["boomPower"]].abilityAmount1 / 100);
                Debug.Log($"Damage : {damage}");
                ore.GetDamage(damage);
            }
        }

    }
}

