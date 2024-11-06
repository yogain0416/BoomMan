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
        
        public IEnumerator Capacity()
        {
            int count = 0;
            
            while (true)
            {
                foreach (var ore in PlayerDataManager.Ore)
                {
                    count += ore.Value;
                }
                _capacitySlider.value = (float)count / PlayerDataManager.PlayerData.capacity;
                
                if (_capacitySlider.value >= 1)
                {
                    // TODO 폭탄 터지면 안됨
                }

                yield return null;
            }
        }

        public IEnumerator BoomTimer(ParticleSystem particleSystem)
        {
            float time = (BoomManData.Define.DefineMap["Boom_Delay"].value * 10) / (9 + PlayerDataManager.PlayerData.boomSpeed + (1 + BoomManData.Upgrade.UpgradeMap[PlayerDataManager.PlayerData.upgradeId["boomSpeed"]].abilityAmount1 / 100));
            float coolTIme = time;
            // TODO 시간 변경
            while (true)
            {
                _boomText.text = coolTIme.ToString("F1");
                coolTIme -= Time.deltaTime;
                if (coolTIme <= 0.0f)
                {
                    // TODO 폭발 효과 및 등등

                    Debug.Log("Boom");
                    particleSystem.Play();
                    coolTIme = time;
                }

                yield return null;
            }
        }
    }
}
