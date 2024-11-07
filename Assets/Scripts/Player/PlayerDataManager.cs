using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json; // Newtonsoft.Json 사용
using UnityEngine;

namespace App.Data
{
    public class PlayerDataManager : MonoBehaviour
    {
        private const string PlayerDataFileName = "PlayerData.json";
        private static PlayerData _playerData;
        private static readonly TimeSpan SaveInterval = TimeSpan.FromMinutes(10);
        private WaitForSeconds _waitForSeconds = new WaitForSeconds((float)SaveInterval.TotalSeconds);

        public static PlayerData PlayerData => _playerData;
        public static bool IsDataLoaded { get; private set; } = false; // 데이터 로드 완료 상태 플래그
        public static Dictionary<string, int> Ore = new Dictionary<string, int>();

        private void InitializeOreDictionary()
        {
            if (BoomManData.OreStatus.OreStatusList != null)
            {
                foreach (var oreStatus in BoomManData.OreStatus.OreStatusList)
                {
                    Ore[oreStatus.id] = 0;
                }
            }
            else
            {
                Debug.LogError("OreStatusList가 초기화되지 않았습니다.");
            }
        }

        private async void Awake()
        {
            await LoadPlayerData();
            InitializeOreDictionary(); // Ore 딕셔너리 초기화
            IsDataLoaded = true; // 데이터 로드 완료 설정
            Debug.Log("PlayerData가 로드된 후 다음 작업을 수행합니다.");

            StartCoroutine(AutoSaveRoutine());
            Application.quitting += OnApplicationQuit; // 앱 종료 시 저장
        }

        private async Task LoadPlayerData()
        {
            // 파일 경로 설정
            string filePath = Path.Combine(Application.persistentDataPath, PlayerDataFileName);

            if (File.Exists(filePath))
            {
                // 파일이 존재할 경우 JSON에서 데이터를 비동기로 읽음
                string jsonData = await File.ReadAllTextAsync(filePath);
                _playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData); // Newtonsoft.Json 사용
                Debug.Log("PlayerData 로드 완료");
            }
            else
            {
                // 파일이 없을 경우 기본값으로 초기화하고 저장
                _playerData = new PlayerData
                {
                    boomRange = BoomManData.PlayerStatus.PlayerStatusList[0].boomRange,
                    boomPower = BoomManData.PlayerStatus.PlayerStatusList[0].boomPower,
                    boomValue = BoomManData.PlayerStatus.PlayerStatusList[0].boomValue,
                    boomSpeed = BoomManData.PlayerStatus.PlayerStatusList[0].boomSpeed,
                    speed = BoomManData.PlayerStatus.PlayerStatusList[0].speed,
                    capacity = BoomManData.PlayerStatus.PlayerStatusList[0].capacity,
                    gold = 0,
                    upgradeId = new()
                    {
                        { "boomRange", "40101" }, { "boomPower", "40201" }, { "boomSpeed", "40301" },
                        { "capacity", "40401" }, { "moveSpeed", "40501" }
                    }
                };

                await SavePlayerDataAsync(); // 비동기로 저장
                Debug.Log("PlayerData 기본값으로 초기화 및 저장");
            }
        }

        private async Task SavePlayerDataAsync()
        {
            string filePath = Path.Combine(Application.persistentDataPath, PlayerDataFileName);
            string jsonData = JsonConvert.SerializeObject(_playerData, Formatting.Indented); // Newtonsoft.Json 사용

            await File.WriteAllTextAsync(filePath, jsonData);
            Debug.Log("PlayerData 파일 저장 완료: " + filePath);
        }

        private IEnumerator AutoSaveRoutine()
        {
            while (true)
            {
                yield return _waitForSeconds;
                SavePlayerData();
                Debug.Log("PlayerData 자동 저장 완료");
            }
        }

        private void SavePlayerData()
        {
            string filePath = Path.Combine(Application.persistentDataPath, PlayerDataFileName);
            string jsonData = JsonConvert.SerializeObject(_playerData, Formatting.Indented); // Newtonsoft.Json 사용

            File.WriteAllText(filePath, jsonData);
            Debug.Log("PlayerData 파일 저장 완료: " + filePath);
        }

        private void OnApplicationQuit()
        {
            // 앱 종료 시 마지막 저장
            SavePlayerData();
        }

        // PlayerData를 수정하는 메서드를 추가 (필요한 경우에만 호출)
        public static void UpdatePlayerData(Action<PlayerData> updateAction)
        {
            updateAction(_playerData);
        }
    }
}