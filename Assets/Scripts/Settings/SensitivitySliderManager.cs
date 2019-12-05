using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensitivitySliderManager : MonoBehaviour
{
    public void ChangedClickSpeed(float newVal)
    {
        PlayerControl.AdjustClickSpeed(newVal);
    }
    public void ChangedMoveSpeed(float newVal)
    {
        PlayerControl.AdjustMoveSpeed(newVal);
    }
}
