using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoughnutShooter : MonoBehaviour
{
    public GameObject Item
    {
        get => item;
        set => item = value;
    }
    
    [SerializeField]
    private GameObject item;

    [SerializeField]
    private Vector2 itemPosition;

    [SerializeField] 
    private float resistanceStrength = 0.7f;

    [SerializeField] 
    private float forceMultiplier = 5;

    [SerializeField] 
    private AudioSource source;

    private Vector2 mouseDownPosition;

    private RandomDoughnutQueue queue => GameplayManager.Current.RandomDoughnutQueue;

    private bool selected;

    private void Start()
    {
        StartCoroutine(addDoughnut());
    }

    void Update()
    {
        if (Item == null)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            if (!selected)
                return;
            
            Vector2 currentMousePosition = Input.mousePosition;

            var change = Camera.main!.ScreenToWorldPoint(currentMousePosition) - Camera.main!.ScreenToWorldPoint(mouseDownPosition);

            change *= change.magnitude <= 0 ? 0 : MathF.Pow(change.magnitude, resistanceStrength) / change.magnitude;

            if (change.magnitude == 0)
                return;

            item.transform.position = (Vector3)itemPosition + change;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            var rigidbody = item.GetComponent<Rigidbody2D>();
            rigidbody.simulated = true;

            item.GetComponent<Collider2D>().enabled = true;
            item.GetComponent<Doughnut>().State = DoughnutState.Moving;

            if (((Vector2)item.transform.position - itemPosition).magnitude < 0.01f)
                return;
            
            rigidbody.AddForce(-((Vector2)item.transform.position - itemPosition) * forceMultiplier, ForceMode2D.Impulse);
            rigidbody.gravityScale = -0.5f;
            
            source.Play();
            item = null;
            this.StartDelayedCoroutine(addDoughnut(), 1f);
        }
    }

    private IEnumerator addDoughnut()
    {
        var scale = queue.Next.Scale;
        var doughnut = queue.Get();
        
        item = doughnut.gameObject;
        item.transform.position = itemPosition;
        
        doughnut.transform.localScale = Vector3.zero;
        doughnut.ScaleTo(new Vector3(scale, scale, scale), 0.7f, Easing.OutElastic);

        yield return null;
    }

    private void OnMouseDown()
    {
        if (Time.timeScale != 0)
        {
            selected = true;
        }
    }

    private void OnMouseUp()
    {
        selected = false;
    }
}
