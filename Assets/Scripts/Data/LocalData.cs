using System;
using System.Globalization;
using UnityEngine;

namespace BoomManData
{
    [Serializable]
    public class LocalData
    {
        public LocalData()
        {
            player = new();
            character = new();
            status = new();
            upgrade = new();
        }

        public PlayerData player;
        public CharacterData character;
        public CharacterStatus status;
        public Upgrade upgrade;
    }

    [Serializable]
    public class PlayerData
    {
        public int level;
        // TODO 
        public Vector3 position;
    }

    [Serializable]
    public class CharacterData
    {
        // TODO 변경 필요
        public int id;
        public string name;
        public string key;
        public string type;
        public string resource;
        public float size;
        public string collider;
        public Vector3 scale;
        public Vector3 range;
    }

    [Serializable]
    public class CharacterStatus
    {
        public int id;
        public string name;
        public float boomRange;
        public float boomPower;
        public float boomValue;
        public float boomSpeed;
        public float speed;
        public int capacity;
        public float[] oreResourceRate;
        public int oreResourceCost;
        public float[] oreStageThreshesholds;
        public int[] oreStageModelId;
    }

    [Serializable]
    public class Upgrade
    {
        public string upgradeType;
        public string key;
        public string boomType;
        public int level;
        public int goldCost;
        public string abilityType1;
        public string abilityType2;
        public float abilityAmount1;
        public float abilityAmount2;
    }
    
}
