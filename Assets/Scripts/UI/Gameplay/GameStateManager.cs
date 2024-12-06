using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseOverlay;      // �Ͻ����� UI

    private bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // ���� ����
            pauseOverlay.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // ���� �簳
            pauseOverlay.SetActive(false);
        }
    }
}
