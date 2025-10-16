using UnityEngine;
using static UnityEngine.UI.Image;

public class ParrotClicker : MonoBehaviour
{
    bool holdingParrot = false;

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
                    Debug.Log("clicked on parrot");
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

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        if (holdingParrot)
        {
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }
    }

}

