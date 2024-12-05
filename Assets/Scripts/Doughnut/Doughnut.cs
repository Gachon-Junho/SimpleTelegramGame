using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class Doughnut : MonoBehaviour
{
    public DoughnutLevel DoughnutLevel;
    
    public int Level
    {
        get => level;
        set
        {
            if (level == value || DoughnutLevel.SpritePerLevel.Count <= level)
                return;
            
            onLevelChanged?.Invoke();
            GameplayManager.Current.Score += Score;
            
            level = value;
            name = $"Level {value}";
            
            var next = DoughnutLevel.SpritePerLevel.FirstOrDefault(d => d.Level == value);

            if (next == null)
                return;
            
            this.ScaleTo(new Vector3(next.Scale, next.Scale, next.Scale), 0.5f, Easing.InOutBack);
            Sprite = next.Sprite;
        }
    }

    public int Score;

    public DoughnutState State;

    public Sprite Sprite
    {
        get => sprite;
        set
        {
            spriteRenderer.sprite = value;
            sprite = value;
        }
    }

    public AudioSource AudioSource;

    private Action onLevelChanged;

    private int level;
    
    [SerializeField]
    private Sprite sprite;

    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    [SerializeField] 
    private AudioClip clip;
    
    public Rigidbody2D Rigidbody => rigidbody ??= GetComponent<Rigidbody2D>();
    
    private Rigidbody2D rigidbody;
    private Vector2 lastVelocity;

    private void Update()
    {
        lastVelocity = Rigidbody.velocity;
        
        if (Rigidbody.velocity.magnitude <= 0.05f)
            State = DoughnutState.Completed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (State == DoughnutState.Completed && other.gameObject.CompareTag("GameOverLine"))
        {
            GameplayManager.Current.FinishGame();
            return;
        }
        
        var newVelocity = Vector2.Reflect(lastVelocity, other.contacts[0].normal);
        
        if (other.gameObject.CompareTag("ConfineWall"))
        {
            Rigidbody.velocity = newVelocity;
        }
        else if (other.gameObject.CompareTag("Doughnut"))
        {
            var d = other.gameObject.GetComponent<Doughnut>();

            Rigidbody.velocity = newVelocity;
            Rigidbody.gravityScale = -1;
            
            // 같은 도넛이고, 자신의 레벨이 낮거나 활발히 움직이고 있다면 사라짐.
            if (level != d.Level || Rigidbody.velocity.magnitude < d.Rigidbody.velocity.magnitude || level == DoughnutLevel.SpritePerLevel.Count)
                return;
            
            d.Level++;
            d.AudioSource.Play();
            Rigidbody.velocity = Vector2.zero;
            
            this.ScaleTo(Vector3.zero, 0.5f, Easing.OutQuint, () => Destroy(gameObject));
        }
        else
        {
            Rigidbody.gravityScale = -1;
            Rigidbody.velocity = Vector2.zero;
            Rigidbody.angularVelocity = 0;
            State = DoughnutState.Completed;
        }
    }
}