
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject parrot;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI eggPriceText;
    [SerializeField] TextMeshProUGUI autoClickPriceText;

    [Header("Spawn Egg")]
    [SerializeField] GameObject egg;
    [SerializeField] Vector2 spawnTransform;
    [SerializeField] int eggPrice = 10;

    [Header("Auto Click")]
    [SerializeField] bool autoClick = false;
    [SerializeField] float autoClickTimer = 2;
    [SerializeField] float currentTime;
    [SerializeField] int autoClickPrice = 50;

    int currentScore = 0;
    bool holdingParrot = false;

    ParrotClicker parrotClicker;


    void Awake()
    {
       parrotClicker = FindFirstObjectByType<ParrotClicker>();
    }

    void Start()
    {

        scoreText.text = currentScore.ToString();
        eggPriceText.text = eggPrice.ToString();
        autoClickPriceText.text = autoClickPrice.ToString();
    }

    void Update()
    {
        ClickOnParrot();

        AutoClick();

        MoveParrot();
    }

    void AutoClick()
    {
        GameObject[] parrotList = GameObject.FindGameObjectsWithTag("Parrot");

        if (autoClick == true)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= autoClickTimer)
            {
                foreach (GameObject parrots in parrotList)
                {
                    parrots.GetComponent<ParrotClicker>().CountFeathers();
                }

                scoreText.text = currentScore.ToString();
                currentTime = 0;
            }
        }
    }

    public void BuyAutoClickUpgrade()
    {
        if(autoClickPrice <= currentScore)
        {
            currentScore -= autoClickPrice;
            autoClickPrice = autoClickPrice * 8;
            autoClickPriceText.text = autoClickPrice.ToString();
            scoreText.text = currentScore.ToString();

            if (autoClick == true)
            {
                autoClickTimer -= 0.3f;
            }

            if (autoClick == true) { return; }

            autoClick = true;
        }

    }

    public void BuyEgg()
    {
        Vector2 spawnPoint = FindValidSpawnPoint();

        if (currentScore >= eggPrice)
        {
            currentScore -= eggPrice;
            eggPrice = eggPrice * 4;
            Instantiate(egg, spawnPoint, Quaternion.identity);
            eggPriceText.text = eggPrice.ToString();
            scoreText.text = currentScore.ToString();
        }
    }


    void ClickOnParrot()
    {
        // Clicks on the parrot
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPosition), Vector2.zero);
            if (hit)
            {
                // hits parrot and adds points
                if (hit && hit.transform.gameObject.tag == "Parrot")
                {
                    parrotClicker = hit.transform.gameObject.GetComponent<ParrotClicker>();
                    parrot = hit.transform.gameObject;
                    parrotClicker.CountFeathers();
                    parrotClicker.TweetSound();
                }

                if (hit && hit.transform.gameObject.tag == "Egg")
                {
                    parrotClicker = hit.transform.gameObject.GetComponent<ParrotClicker>();
                    parrotClicker.OpenEgg();
                }

            }
        }

        // This in the method to check if the mouse holds the parrot
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 clickPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPosition), Vector2.zero);

            if (hit && hit.transform.gameObject.tag == "Parrot")
            {
                parrotClicker = hit.transform.gameObject.GetComponent<ParrotClicker>();
                parrot = hit.transform.gameObject;
                holdingParrot = true;
            }

            if (hit && hit.transform.gameObject.tag == "Egg")
            {
                parrotClicker = hit.transform.gameObject.GetComponent<ParrotClicker>();
                parrot = hit.transform.gameObject;
                holdingParrot = true;
            }
        }
        // if not holding down the mouse button you stop moving the parrot
        if (Input.GetMouseButtonUp(1))
        {
            holdingParrot = false;
        }
    }

    void MoveParrot()
    {
        if (!holdingParrot) { return; }

        //Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;
        if (holdingParrot)
        {
            //parrot.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            parrot.GetComponent<Rigidbody2D>().MovePosition(mousePos);
        }
    }

    public void AddToScore(int points)
    {
        currentScore += points;
        scoreText.text = currentScore.ToString();
    }


    Vector2 FindValidSpawnPoint()
    {
        int maxAttempts = 50;
        float minDistance = 1.5f; // Minimum distance from other colliders
        Vector2 spawnPoint = Vector2.zero;

        for (int i = 0; i < maxAttempts; i++)
        {
            // Random position within your desired area
            spawnPoint = new Vector2(Random.Range(-9f, 4f), Random.Range(-5f, 5f));

            // Check for nearby colliders 
            Collider2D hit = Physics2D.OverlapCircle(spawnPoint, minDistance);

            if (hit == null)
            {
                // Found a free spot
                return spawnPoint;
            }
        }

        Debug.LogWarning("Could not find a valid spawn point after many attempts!");
        return spawnPoint; 
    }

    public bool StopHoldingParrot()
    {
        holdingParrot = false;
        return holdingParrot;
    }

}
