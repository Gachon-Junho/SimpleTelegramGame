using System;
using UnityEngine;

public class DangerLine : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        var doughnut = other.GetComponent<Doughnut>();

        if (doughnut == null)
            return;
        
        if (doughnut.State == DoughnutState.Completed)
            GameplayManager.Current.FinishGame();
    }
}