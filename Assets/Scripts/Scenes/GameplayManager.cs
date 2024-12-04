using System;
using TMPro;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    public RandomDoughnutQueue RandomDoughnutQueue => randomDoughnutQueue;

    public int Score
    {
        get => score;
        set
        {
            score = value;

            scoreText.text = $"{score:D8}";
        }
    }

    private int score;

    [SerializeField]
    private TMP_Text scoreText;
    public DoughnutShooter Shooter => shooter;
    
    [SerializeField]
    private RandomDoughnutQueue randomDoughnutQueue;

    [SerializeField] 
    private DoughnutShooter shooter;

    private void Start()
    {
    }
}