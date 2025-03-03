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
            Application.targetFrameRate = 60; // ���� 120 �� ���, ������ 60
        }

        QualitySettings.vSyncCount = 0;
    }

}
