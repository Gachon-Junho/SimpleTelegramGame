using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseOverlay;      // 일시정지 UI

    private bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // 게임 멈춤
            pauseOverlay.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // 게임 재개
            pauseOverlay.SetActive(false);
        }
    }
}
