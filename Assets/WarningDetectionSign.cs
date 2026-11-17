using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WarningDetectionSign : MonoBehaviour
{
    [SerializeField] GameObject sign;
    [SerializeField] VisionAgent1 _VisionAgent1;
    private void Start()
    {
        _VisionAgent1 = GetComponent<VisionAgent1>();
        _VisionAgent1.OnDetected += ShowSign;
        _VisionAgent1.OnLoseDetection += HideSign;

    }
    public void ShowSign()
    {
        sign.SetActive(true);
    }

    public void HideSign()
    {
        sign.SetActive(false);
    }
}
