using System;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    public RandomDoughnutQueue RandomDoughnutQueue => randomDoughnutQueue;
    public DoughnutShooter Shooter => shooter;
    
    [SerializeField]
    private RandomDoughnutQueue randomDoughnutQueue;

    [SerializeField] 
    private DoughnutShooter shooter;

    private void Start()
    {
    }
}