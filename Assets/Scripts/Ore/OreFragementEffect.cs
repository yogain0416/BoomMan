using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace App.Player
{
    public class OreFragementEffect : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _oreCountText;
        [SerializeField] private Image _oreImage;

        public void SetUIs(int desc, Sprite sprite)
        {
            _oreCountText.text = "+" + desc;
            _oreImage.sprite = sprite;
        }
    }
}