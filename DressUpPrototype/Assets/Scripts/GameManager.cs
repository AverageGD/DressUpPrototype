using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        int maxRefreshRate = Screen.currentResolution.refreshRate;

        if (maxRefreshRate >= 120)
        {
            Application.targetFrameRate = 120;
        }
        else
        {
            Application.targetFrameRate = 60; // Если 120 Гц нет, ставим 60
        }

        QualitySettings.vSyncCount = 0;
    }

}
