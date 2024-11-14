﻿using System;
using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Rigidbody2D Rigidbody => rigidbody ??= GetComponent<Rigidbody2D>();
    
    private Rigidbody2D rigidbody;

    private Vector2 lastVelocity;

    private void Update()
    {
        Debug.Log(Rigidbody.velocity);
        lastVelocity = Rigidbody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision Enter");
        
        if (other.gameObject.CompareTag("ConfineWall"))
        {
            var newVelocity = Vector2.Reflect(lastVelocity, other.contacts[0].normal);
            Rigidbody.velocity = newVelocity;
        }
    }
}