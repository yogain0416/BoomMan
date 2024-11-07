using App.UI;
using App.Data;

using UnityEngine;

namespace App.Player
{
    public class Ore : MonoBehaviour
    {
        [SerializeField] private OreData _data;
        [SerializeField] private OreUI _oreUI;

        [SerializeField] private GameObject _oreBase;
        [SerializeField] private GameObject _oreBreak1;
        [SerializeField] private GameObject _oreBreak2;

        [SerializeField] private Material _material;
        
        [SerializeField] private float _maxHp;
        private float _hp;
        private float _sumDamage;
        
        public float Hp { get { return _hp; } set { _hp = value; } }

        private void Start()
        {
            _hp = _maxHp;
            SetText();
            SetMaterial();
            SetOreActive();
        }

        public void GetDamage(float damage)
        {
            Hp -= damage;
            
            // 광물 획득
            GetOre(damage);

            if (Hp <= 0)
            {
                Hp = 0;
                Destroy(this.gameObject);
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
    }
}