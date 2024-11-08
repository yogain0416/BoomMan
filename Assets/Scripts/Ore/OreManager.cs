using UnityEngine;

using System.Collections.Generic;

namespace App.Player
{
    public class OreManager : MonoBehaviour
    {
        void Start()
        {
            // 삭제된  광물 ID 목록 불러오기
            List<string> destroyedOres = LoadDestroyedOres();

            // 모든 광물를 찾아서 삭제된 광물은 비활성화
            foreach (Ore ore in FindObjectsOfType<Ore>())
            {
                if (destroyedOres.Contains(ore.ID))
                {
                    ore.gameObject.SetActive(false);
                }
            }
        }

        private List<string> LoadDestroyedOres()
        {
            string savedData = PlayerPrefs.GetString("DestroyedOres", "");
            if (string.IsNullOrEmpty(savedData)) return new List<string>();

            return new List<string>(savedData.Split(','));
        }
    }
}