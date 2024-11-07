using System.Collections.Generic;
using System.Linq;
using App.Data;
using App.UI;
using UnityEngine;

namespace App.Player
{
    public class Bank : MonoBehaviour
    {
        [SerializeField] private BasicUI _basicUI;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("재화 Update");

                // 돈 넣기
                string[] keys = PlayerDataManager.Ore.Keys.ToArray();
                for (int i = 0;  i < keys.Length; i++)
                {
                    PlayerDataManager.PlayerData.gold += PlayerDataManager.Ore[keys[i]] * BoomManData.OreStatus.OreStatusMap[keys[i]].oreResourceCost;
                    PlayerDataManager.Ore[keys[i]] = 0;
                }

                _basicUI.SetTexts();
            }
        }
    }
}