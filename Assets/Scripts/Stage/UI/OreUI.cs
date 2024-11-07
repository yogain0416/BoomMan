using TMPro;

using UnityEngine;

namespace App.UI
{
    public class OreUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _hpText;

        public void SetHpText(string hpText)
        {
            _hpText.text = hpText;
        }
    }
}