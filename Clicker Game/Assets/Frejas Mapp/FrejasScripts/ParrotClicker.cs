
using UnityEngine;

public class ParrotClicker : MonoBehaviour
{
    [SerializeField] int pointsWhenPressed = 1;

    [Header("Egg")]
    [SerializeField] int timesHit = 0;
    [SerializeField] SpriteRenderer eggSprite;
    [SerializeField] SpriteRenderer crackedEggSprite;
    [SerializeField] SpriteRenderer babySprite;

    [Header("Parrot Prefabs")]
    [SerializeField] GameObject parrot1;
    [SerializeField] GameObject parrot2;
    [SerializeField] GameObject parrot3;
    [SerializeField] GameObject parrot4;

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

    void FixedUpdate()
    {
        parrotRigidbody.linearVelocity = Vector3.zero;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // merge parrot 1 and spawn parrot 2
        if (other.gameObject.layer == 7 && gameObject.layer == 7)
        {
            SpawnParrot2();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        // merge parrot 2 and spawn parrot 3
        if (other.gameObject.layer == 8 && gameObject.layer == 8)
        {
            SpawnParrot3();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        // merge parrot 3 and spawn parrot 4
        if (other.gameObject.layer == 9 && gameObject.layer == 9)
        {
            SpawnParrot4();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }



    public void CountFeathers()
    {
        gameManager.AddToScore(pointsWhenPressed);
    }

    public void OpenEgg()
    {
        timesHit += 1;

        if (timesHit >= 3)
        {
            eggSprite.enabled = false;
            crackedEggSprite.enabled = true;
        }
        if (timesHit >= 6)
        {
            crackedEggSprite.enabled = false;
            babySprite.enabled = true;
        }
        if (timesHit >= 9)
        {
            Instantiate(parrot1, transform.position, Quaternion.identity);
            gameManager.StopHoldingParrot();
            Destroy(gameObject);
        }
    }


    void SpawnParrot2()
    {
        if (gameManager.parrot == this.gameObject)
        {
            Instantiate(parrot2, transform.position, Quaternion.identity);
            gameManager.StopHoldingParrot();
        }
    }

    void SpawnParrot3()
    {
        if (gameManager.parrot == this.gameObject)
        {
            Instantiate(parrot3, transform.position, Quaternion.identity);
            gameManager.StopHoldingParrot();
        }
    }

    void SpawnParrot4()
    {
        if (gameManager.parrot == this.gameObject)
        {
            Instantiate(parrot4, transform.position, Quaternion.identity);
            gameManager.StopHoldingParrot();
        }
    }
}

