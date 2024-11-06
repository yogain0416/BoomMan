using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Initialization
{
    using System.Collections;
    using BoomManData;
    using UGS;
    using UnityEngine;
    using UnityEngine.Networking;

    public class Initialization : MonoBehaviour
    {
        // TODO 버전 비교하는 것도 UGS로 변경하기
        private const string URL = "https://script.google.com/macros/s/AKfycbz63-yEQYoKKBKkoUtHX_fgmZ2LtceufTHaVS6M4cMlDS0Ge5enbH_MGy4e4R_wYeMT/exec";

        // 게임의 현재 버전, 실제 게임에서는 PlayerPrefs에서 저장된 버전을 불러올 수 있습니다.
        private string _version = "1.0";

        void Start()
        {
            // TODO 처음인지 아닌지 확인 후
            // 처음이면 파일 생성 해주고
            // 처음이 아니면 버전 확인해서 관련 DB들 업데이트 및 그대로 하기
            
            UnityGoogleSheet.LoadAllData();

            if (PlayerPrefs.GetString("GameVersion").Equals(_version)) Debug.Log("버전 같음"); // return;
            StartCoroutine(CheckForUpdates());
        }

        private IEnumerator CheckForUpdates()
        {
            using (UnityWebRequest request = UnityWebRequest.Get(URL))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error fetching version data: " + request.error);
                    yield break;
                }

                // 스프레드 시트에서 가져온 버전 (예를 들어 첫 번째 값이 버전 값이라고 가정)
                var sheetVersion = request.downloadHandler.text;

                Debug.Log("Current Version: " + _version);
                Debug.Log("Sheet Version: " + sheetVersion);

                if (_version != sheetVersion)
                {
                    // 버전이 다르면 업데이트
                    UpdateGameVersion(sheetVersion);

                    // TODO 데이터 값들 업데이트

                }
                else
                {
                    // TODO 맨 처음 게임을 다운 했을 때 버전이 같은지 다른 지 확인
                    Debug.Log("Version is up-to-date.");
                }
            }
        }

        private void UpdateGameVersion(string newVersion)
        {
            _version = newVersion;
            PlayerPrefs.SetString("GameVersion", _version);
            PlayerPrefs.Save();
            Debug.Log("Game version updated to: " + _version);
        }

        // TODO 업데이트 로딩
    }
}
