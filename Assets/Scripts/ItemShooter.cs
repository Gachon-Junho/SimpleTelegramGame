using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemShooter : MonoBehaviour
{
    public GameObject Item
    {
        get => item;
        set => item = value;
    }
    
    [SerializeField]
    private GameObject item;

    private Vector2 mouseDownPosition;
    private Vector2 previousMousePosition;
    private Vector2 delta;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;
            previousMousePosition = Input.mousePosition;
            Debug.Log(mouseDownPosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePosition = Input.mousePosition;
            delta = currentMousePosition - previousMousePosition;

            var change = Camera.main!.ScreenToWorldPoint(Input.mousePosition) - Camera.main!.ScreenToWorldPoint(mouseDownPosition);
            
            Debug.Log(Input.mousePosition);
            Debug.Log($"change: {change}");

            change *= change.magnitude <= 0 ? 0 : MathF.Pow(change.magnitude, 0.7f) / change.magnitude;

            if (change.magnitude == 0)
                return;

            item.transform.position = new Vector3(0, -3.5f) + change + new Vector3(0, 0, 10);

            // item.transform.position = Vector3.MoveTowards(item.transform.position,
            //     Camera.main!.ScreenToWorldPoint(change), float.MaxValue) + new Vector3(0, 0, 10);

            previousMousePosition = currentMousePosition;
        }
    }
}
