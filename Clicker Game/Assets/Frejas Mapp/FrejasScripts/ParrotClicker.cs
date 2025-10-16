using UnityEngine;
using static UnityEngine.UI.Image;

public class ParrotClicker : MonoBehaviour
{
    [SerializeField] int pointsWhenPressed = 1;

    Rigidbody2D parrotRigidbody;
    GameManager gameManager;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Start()
    {
        parrotRigidbody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Colliders collide");
        parrotRigidbody.linearVelocity = Vector2.zero;
        if (other.gameObject.layer == 4)
        {
            Debug.Log("Can Merge Parrots");
        }
    }

    public void CountFeathers()
    {
        gameManager.AddToScore(pointsWhenPressed);
    }

    

}

