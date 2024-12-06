using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReloader : MonoBehaviour
{
    public void ReloadScene()
    {
        // Scene ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
