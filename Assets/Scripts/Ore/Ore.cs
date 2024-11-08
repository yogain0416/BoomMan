using App.UI;
using App.Data;

using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using App.Initialization;

namespace App.Player
{
    public class Ore : MonoBehaviour
    {
        [SerializeField] private OreData _data;
        [SerializeField] private OreUI _oreUI;
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private BasicUI _basicUI;

        [SerializeField] private GameObject _oreBase;
        [SerializeField] private GameObject _oreBreak1;
        [SerializeField] private GameObject _oreBreak2;

        [SerializeField] private Material _material;
        
        [SerializeField] private float _maxHp;
        
        [SerializeField] public string ID;

        private float _hp;
        private float _sumDamage;
        private bool _isAttacked;
        private IEnumerator _delayHandler = null;
        public float Hp { get { return _hp; } set { _hp = value; } }

        private void Start()
        {
            _hp = _maxHp;
            SetText();
            SetMaterial();
            SetOreActive();
        }

        private void GetDamage(float damage)
        {
            Hp -= damage;
            
            // 광물 획득
            GetOre(damage);

            if (Hp <= 0)
            {
                Hp = 0;
                SaveOreAsDestroyed();
                if (_delayHandler != null) StopCoroutine(_delayHandler);
                gameObject.SetActive(false); // 비활성화
                return;
            }

            // 광물 상태를 위한 비율
            float baseHp = _maxHp * (1 - _data.oreStageThresholds / 100.0f);
            float breakHp = _maxHp * (1 - _data.oreStageThresholds * 2 / 100.0f);
            bool baseActive = Hp > baseHp ? true : false;
            bool breakActive = baseHp >= Hp && Hp > breakHp ? true : false;
            SetOreActive(baseActive, breakActive);
            SetText();
        }

        private void SetText()
        {
            _oreUI.SetHpText(Mathf.CeilToInt(Hp).ToString());
        }

        private void SetOreActive(bool baseActive = true, bool breakActive = false)
        {
            _oreBase.SetActive(baseActive);
            _oreBreak1.SetActive(breakActive);
            _oreBreak2.SetActive(baseActive == false && breakActive == false);
        }

        private void SetMaterial()
        {
            if (_material == null) return;

            _oreBase.GetComponent<MeshRenderer>().material = _material;
            _oreBreak1.GetComponent<MeshRenderer>().material = _material;
            _oreBreak2.GetComponent<MeshRenderer>().material = _material;
        }

        private void GetOre(float damage)
        {
            _sumDamage += damage;
            
            // Hp를 뚫은 데미지는 다시 빼준다
            if (Hp <= 0) _sumDamage += Hp;

            float result = _sumDamage / _data.oreResourceRate;

            // 광물 획득
            if (result >= 1)
            {               
                int oreCount = GetCountOre();
                if (oreCount >= PlayerDataManager.PlayerData.capacity) return;

                int remainCapacity = PlayerDataManager.PlayerData.capacity - oreCount;
                if (remainCapacity >= (int)result)
                    PlayerDataManager.Ore[_data.id] += (int)result;
                else
                    PlayerDataManager.Ore[_data.id] += remainCapacity;

                _sumDamage %= _data.oreResourceRate;
                SoundManager.Instance.Play("Sounds/CM_get_ore", SoundManager.SoundType.OreCollected);
                _basicUI.SetTexts();
            }
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

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Boom" && _playerUI.HasBoom && _isAttacked == false)
            {
                _isAttacked = true;
                Debug.Log("광석 : " + gameObject.name);
                float damage = PlayerDataManager.PlayerData.boomPower * (1 + BoomManData.Upgrade.UpgradeMap[PlayerDataManager.PlayerData.upgradeId["boomPower"]].abilityAmount1 / 100);
                Debug.Log($"Damage : {damage}");
                if (_delayHandler != null) StopCoroutine( _delayHandler );
                StartCoroutine(_delayHandler = CoDelay());
                GetDamage(damage);
            }
        }

        private IEnumerator CoDelay()
        {
            yield return _playerUI._sleepTime;

            _isAttacked = false;
        }

        private void SaveOreAsDestroyed()
        {
            // 기존 삭제된 광물 목록을 불러옴
            List<string> destroyedOres = LoadDestroyedOres();

            // 현재 광물 ID 추가
            if (!destroyedOres.Contains(ID))
            {
                destroyedOres.Add(ID);
            }

            // PlayerPrefs에 다시 저장
            PlayerPrefs.SetString("DestroyedOres", string.Join(",", destroyedOres));
            PlayerPrefs.Save();
        }

        // 삭제된 광물 목록을 불러오는 함수
        private List<string> LoadDestroyedOres()
        {
            string savedData = PlayerPrefs.GetString("DestroyedOres", "");
            if (string.IsNullOrEmpty(savedData)) return new List<string>();

            return new List<string>(savedData.Split(','));
        }
    }
}