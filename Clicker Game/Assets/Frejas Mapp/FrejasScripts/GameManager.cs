using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] public GameObject parrot;

    [SerializeField] bool autoClick = false;
    [SerializeField] bool startTimer = false;
    [SerializeField] float autoClickTimer = 2;
    [SerializeField] float currentTime;
    

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
            startTimer = true;

            if (startTimer == true)
            {
                currentTime += Time.deltaTime;

                if(currentTime >= autoClickTimer)
                {
                    Debug.Log("This autockicks the parrot");

                    foreach (GameObject parrots in parrotList)
                    {
                        parrots.GetComponent<ParrotClicker>().CountFeathers();
                    }

                    startTimer = false;
                    currentTime = 0;
                }
            }
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

    IEnumerator WaitForAutoClick()
    {
        yield return new WaitForSeconds(1f);
    }

    public bool StopHoldingParrot()
    {
        holdingParrot = false;
        return holdingParrot;
    }

}
