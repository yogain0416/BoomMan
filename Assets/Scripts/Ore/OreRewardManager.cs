using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace App.Player
{
    public class OreRewardManager : MonoBehaviour
    {
        public static OreRewardManager Instance { get; private set; } // 싱글톤 인스턴스

        private Camera _mainCamera; // 주 카메라
        [SerializeField] private Canvas _overlayCanvas; // Overlay Canvas
        [SerializeField] private GameObject _oreFragmentPrefab; // 광물 조각 프리팹 (UI Image로 설정)
        [SerializeField] private Sprite _oreLevel1;
        [SerializeField] private Sprite _oreLevel2;
        [SerializeField] private Sprite _oreLevel3;
        [SerializeField] private Sprite _oreLevel4;

        private void Awake()
        {
            // 싱글톤 인스턴스 설정
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("Duplicate instance of OreFragmentEffect detected, destroying the new one.");
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void OnDestroy()
        {
            // 인스턴스 초기화
            if (Instance == this)
            {
                Instance = null;
            }
        }

        // 광물 조각을 생성하고 애니메이션을 시작하는 함수
        public void ShowOreFragment(Vector3 orePosition, string oreID, int oreCount)
        {
            // 월드 좌표 -> 화면 좌표 변환
            Vector3 screenPosition = _mainCamera.WorldToScreenPoint(orePosition);

            // 광물 조각 인스턴스 생성
            GameObject fragment = Instantiate(_oreFragmentPrefab, _overlayCanvas.transform);
            fragment.GetComponent<RectTransform>().position = screenPosition;

            // UI 세팅
            var oreFragementEffect = fragment.GetComponent<OreFragementEffect>();
            SetUIs(oreID, oreCount, oreFragementEffect);

            // 코루틴 시작 (위로 이동하며 사라짐)
            StartCoroutine(AnimateFragment(fragment));
        }

        // 광물 조각이 위로 올라가면서 사라지는 애니메이션
        private IEnumerator AnimateFragment(GameObject fragment)
        {
            RectTransform rectTransform = fragment.GetComponent<RectTransform>();

            Vector3 startPos = rectTransform.position;
            Vector3 endPos = startPos + Vector3.up * 100f; // 위로 100픽셀 이동
            float duration = 1f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                // 위치와 투명도 조정
                rectTransform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 애니메이션 종료 후 조각 제거
            Destroy(fragment);
        }

        private void SetUIs(string oreID, int count, OreFragementEffect oreFragementEffect)
        {
            // TODO 하드코딩 변경필요
            switch (oreID)
            {
                case "20101":
                    oreFragementEffect.SetUIs(count, _oreLevel1);
                    break;
                case "20102":
                    oreFragementEffect.SetUIs(count, _oreLevel2);
                    break;
                case "20103":
                    oreFragementEffect.SetUIs(count, _oreLevel3);
                    break;
                case "20104":
                    oreFragementEffect.SetUIs(count, _oreLevel4);
                    break;
            }
        }
    }
}
