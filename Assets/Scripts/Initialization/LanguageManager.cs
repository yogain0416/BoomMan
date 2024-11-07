using UnityEngine;

namespace App.Initialization
{
    public class LanguageManager
    {
        // 싱글톤 인스턴스
        private static LanguageManager _instance;
        public static LanguageManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LanguageManager();
                    // 이거 처음 Initialization에서 LoadAllData함
                    //_instance.LoadLanguageData();
                }
                return _instance;
            }
        }

        // 외부에서 인스턴스 생성을 막기 위해 private 생성자 사용
        private LanguageManager() { }

        // 언어 데이터를 로드하는 함수
        private void LoadLanguageData()
        {
            // Localization.Data 내부의 Dictionary를 통해 데이터를 자동으로 로드합니다.
            var dataDictionary = Localization.Data.GetDictionary();

            if (dataDictionary == null || dataDictionary.Count == 0)
            {
                Debug.LogError("Language data could not be loaded from Localization.Data.");
            }
            else
            {
                Debug.Log("Language data loaded successfully from Localization.Data.");
            }
        }

        // id로 문자열을 조회하는 함수
        public string GetString(string id)
        {
            // Localization.Data의 Dictionary에서 id를 기반으로 문자열을 직접 조회합니다.
            if (Localization.Data.DataMap.TryGetValue(id, out var data))
            {
                return data.eng;
            }
            else
            {
                Debug.LogWarning($"String ID '{id}' not found in Localization.Data.");
                return string.Empty;
            }
        }

        // 포맷 파라미터를 받는 문자열 조회 함수
        public string GetString(string id, params object[] args)
        {
            string baseString = GetString(id);
            if (string.IsNullOrEmpty(baseString))
            {
                return string.Empty;
            }

            // 파라미터로 받은 값들을 포맷에 맞게 삽입
            return string.Format(baseString, args);
        }
    }
}
