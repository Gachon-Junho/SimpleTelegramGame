using System;
using System.Collections;
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
           StopAllCoroutines();
           StartCoroutine(transformLoop(value, Time.time, Time.time + 1f));
           
           score = value;

           IEnumerator transformLoop(int to, double startTime, double endTime)
           {
               var start = score;

                while (Time.time < endTime)
                {
                    // 텍스트 배열 업데이트
                    foreach (var text in scoreText)
                    {
                        text.text = $"{Interpolation.ValueAt(Time.time, start, to, startTime, endTime, new EasingFunction(Easing.OutQuint)):D6}";
                    }

                    yield return null;
                }
            }
        }
    }

    private int score;

    [SerializeField]
    private TMP_Text[] scoreText;
    public DoughnutShooter Shooter => shooter;
    
    [SerializeField]
    private RandomDoughnutQueue randomDoughnutQueue;

    [SerializeField] 
    private DoughnutShooter shooter;

    [SerializeField] 
    private GameObject gameOverOverlay;



    private void Start()
    {
    }

    public void FinishGame()
    {
        gameOverOverlay.SetActive(true);
        
        foreach (var doughnut in FindObjectsByType<Doughnut>(FindObjectsSortMode.None))
        {
            doughnut.ScaleTo(Vector3.zero, 0.5f, Easing.OutQuint, () => Destroy(doughnut.gameObject));
        }
    }
}