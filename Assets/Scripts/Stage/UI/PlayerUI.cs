using System.Collections;
using App.Data;
using TMPro;
using UGS;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Slider _capacitySlider;
        [SerializeField] private TextMeshProUGUI _boomText;
        [SerializeField] private TextMeshProUGUI _capacityText;

        private WaitForSeconds _sleepTime = new WaitForSeconds(0.1f);
        private bool _isStopBoom = false;
        public bool IsStopBoom { get { return _isStopBoom; } set { _isStopBoom = value; } }
        private bool _hasBoom = false;
        public bool HasBoom { get { return _hasBoom; } set { _hasBoom = value; } }

        private void SetCapacityText()
        {
            _capacityText.text = GetCountOre() + "/" + PlayerDataManager.PlayerData.capacity;
        }

        private int GetCountOre()
        {
            int count = 0;
            foreach (var ore in PlayerDataManager.Ore)
            {
                count += ore.Value;
            }

            return count;
        }

        // TODO 광석
        public IEnumerator Capacity()
        {
            while (true)
            {
                SetCapacityText();

                int count = GetCountOre();

                if (count >= PlayerDataManager.PlayerData.capacity) count = PlayerDataManager.PlayerData.capacity;
                _capacitySlider.value = (float)count / PlayerDataManager.PlayerData.capacity;

                if (_capacitySlider.value >= 1) _isStopBoom = true;
                else _isStopBoom = false;

                yield return null;
            }
        }

        public IEnumerator Boom(ParticleSystem particleSystem)
        {
            float time = (BoomManData.Define.DefineMap["Boom_Delay"].value * 10) / (9 + PlayerDataManager.PlayerData.boomSpeed + (1 + BoomManData.Upgrade.UpgradeMap[PlayerDataManager.PlayerData.upgradeId["boomSpeed"]].abilityAmount1 / 100));
            float coolTime = time;
            
            while (true)
            {
                if (_isStopBoom)
                {
                    yield return null;
                    continue;
                }

                _boomText.text = coolTime.ToString("F1");
                coolTime -= Time.deltaTime;
                if (coolTime <= 0.0f)
                {
                    // TODO 폭발 효과 및 등등
                    Debug.Log("Boom");
                    _hasBoom = true;
                    particleSystem.Play();
                    yield return _sleepTime;
                    _hasBoom = false;
                    coolTime = time;
                }

                yield return null;
            }
        }
    }
}
