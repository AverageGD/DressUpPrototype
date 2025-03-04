using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {

        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;

        QualitySettings.vSyncCount = 0;
    }

}
