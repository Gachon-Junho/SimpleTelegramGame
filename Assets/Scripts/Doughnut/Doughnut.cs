using System;
using System.Collections;
using UnityEngine;

public class Doughnut : MonoBehaviour
{
    public int Level
    {
        get => level;
        set
        {
            if (level == value)
                return;
            
            onLevelChanged?.Invoke();
            
            level = value;
            
            this.ScaleTo(transform.localScale + new Vector3(0.3f, 0.3f, 0.3f), 0.6f, Easing.InOutBack);
            Rigidbody.mass += value;
        }
    }

    public Sprite Sprite
    {
        get => sprite;
        set
        {
            spriteRenderer.sprite = value;
            sprite = value;
        }
    }

    public int Type
    {
        get => type;
        set
        {
            type = value;
        }
    }

    private Action onLevelChanged;

    private int level = 1;

    [SerializeField]
    private int type;
    
    [SerializeField]
    private Sprite sprite;

    [SerializeField] 
    private SpriteRenderer spriteRenderer;
    
    public Rigidbody2D Rigidbody => rigidbody ??= GetComponent<Rigidbody2D>();
    
    private Rigidbody2D rigidbody;
    private Vector2 lastVelocity;

    private void Awake()
    {
    }

    private void Update()
    {
        lastVelocity = Rigidbody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Rigidbody.velocity == Vector2.zero)
            return;
        
        var newVelocity = Vector2.Reflect(lastVelocity, other.contacts[0].normal);
        
        if (other.gameObject.CompareTag("ConfineWall"))
        {
            Rigidbody.velocity = newVelocity;
        }
        else if (other.gameObject.CompareTag("Doughnut"))
        {
            var d = other.gameObject.GetComponent<Doughnut>();
            
            Debug.Log($"{d}, {Rigidbody.gravityScale}");

            Rigidbody.velocity = newVelocity;
            Rigidbody.gravityScale = -1;
            
            // 같은 도넛이고, 자신의 레벨이 낮거나 활발히 움직이고 있다면 사라짐.
            if (type != d.Type || level > d.Level || Rigidbody.velocity.magnitude < d.Rigidbody.velocity.magnitude)
                return;
            
            d.Level++;
            Rigidbody.velocity = Vector2.zero;
            
            this.ScaleTo(Vector3.zero, 0.25f, Easing.OutPow10, () => Destroy(gameObject));
        }
        else
        {
            Rigidbody.gravityScale = -1;
            Rigidbody.velocity = Vector2.zero;
            Rigidbody.angularVelocity = 0;
        }
    }
}