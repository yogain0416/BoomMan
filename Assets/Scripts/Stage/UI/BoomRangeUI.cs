using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomRangeUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    public void SetRange(float range)
    {
        _rectTransform.localScale = Vector3.one * range;
    }
}
