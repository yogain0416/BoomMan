using UnityEngine;

namespace App.Data
{
    [CreateAssetMenu(fileName = "OreData", menuName = "Data/OreData")]
    public class OreData : ScriptableObject
    {
        public string id;
        public string name;
        public int oreResourceRate;
        public int oreResourceCost;
        public int oreStageThresholds;
    }
}