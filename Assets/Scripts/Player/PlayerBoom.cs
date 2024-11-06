using System.Collections;
using System.Collections.Generic;
using App.Data;
using App.UI;
using TMPro;
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
            
            // TODO
            // 파일에 있는 값 가져오기
            StartCoroutine(_playerUI.BoomTimer(_particleSystem));
            _rangeUI.SetRange(PlayerDataManager.PlayerData.boomRange);
        }
    }
}

