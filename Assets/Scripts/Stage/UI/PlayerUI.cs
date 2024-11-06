using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Slider _capacitySlider;
        [SerializeField] private TextMeshProUGUI _boomText;
        
        // TODO 일시정지 되면 코루틴 일시 중단하기
        public float _time = 10.0f;

        public IEnumerator Capacity()
        {
            while (true)
            {
                // TODO 가방 용량으로 크기 조절하기
                _capacitySlider.value += Time.deltaTime;
                if ( _capacitySlider.value >= 1 ) _capacitySlider.value = 0;

                yield return null;
            }
        }

        public IEnumerator BoomTimer()
        {
            // TODO 시간 변경
            while (true)
            {
                _boomText.text = _time.ToString("F1");
                _time -= Time.deltaTime;
                if (_time <= 0.0f)
                {
                    // TODO 폭발 효과 및 등등

                    Debug.Log("Boom");
                    _time = 10.0f;
                }

                yield return null;
            }
        }
    }
}
