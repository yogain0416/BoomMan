using System;
using System.Collections.Generic;

namespace App.Data
{
    public class LocalData
    {

    }

    [Serializable]
    public class PlayerData
    {
        public float boomRange;
        public float boomPower;
        public float boomValue;
        public float boomSpeed;
        public float speed;
        public int capacity;
        public int gold;
        public Dictionary<string, string> upgradeId;
        // TODO 폭탄 단계
    }
}
