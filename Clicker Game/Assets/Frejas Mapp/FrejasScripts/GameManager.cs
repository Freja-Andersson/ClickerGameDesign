using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] public GameObject parrot;

    int currentScore = 0;
    bool holdingParrot = false;

    ParrotClicker parrotClicker;

    void Awake()
    {
       // parrotClicker = FindFirstObjectByType<ParrotClicker>();
    }

    void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    void Update()
    {
        ClickOnParrot();

        MoveParrot();
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
                if (hit && hit.transform.gameObject.tag == "Parrot")
                {
                    parrotClicker = hit.transform.gameObject.GetComponent<ParrotClicker>();
                    parrot = hit.transform.gameObject;
                    parrotClicker.CountFeathers();
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
                Debug.Log("Found object to move");
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

    public bool StopHoldingParrot()
    {
        Debug.Log("Holding parrot becomes false");
        holdingParrot = false;
        return holdingParrot;
    }

}
