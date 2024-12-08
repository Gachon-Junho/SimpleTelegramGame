using System;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject scrollContent;

    [SerializeField]
    private GameObject rankCard;

    private void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            Instantiate(rankCard, scrollContent.transform);
        }
    }
}