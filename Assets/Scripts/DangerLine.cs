using System;
using System.Collections;
using UnityEngine;

public class DangerLine : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        var doughnut = other.GetComponent<Doughnut>();

        if (doughnut == null)
            return;

        if (doughnut.State == DoughnutState.Completed)
            this.StartDelayedCoroutine(finish(), 1);
    }

    private IEnumerator finish()
    {
        GameplayManager.Current.FinishGame();

        yield return null;
    }
}